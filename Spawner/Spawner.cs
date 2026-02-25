/*
 Author: Gary Lougheed
 Date Created: ~2/08/2026
 Last Edit: ~2/24/2026 - by Gary Lougheed
 Version: 3
 Dependencies: GameObjectPool, PooledGameObject
 Description: A generic spawner used to spawn game objects.
*/


using GameDevTV;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class Spawner : MonoBehaviour
    {
 
       [Header("Spawner Settings")]
       [SerializeField] SpawnerConfig m_SpawnerConfig;
       /*[SerializeField] private bool m_repeatSpawn = false; // Whether to repeat the spawning process after the initial spawn.
       [SerializeField, Range(1,100)] private int  m_numOfSpawnWavesRequested = 1;
       [SerializeField] private bool m_randomizeSpawnDelay = false;
       [SerializeField] private bool m_randomizeSpawnCycleDelay = false;
       [SerializeField] private float m_SpawnCycleDelay = 5f; // Time delay between spawn cycles if repeatSpawn is true.
       [SerializeField, Range(0.0f,3f)] private float m_spawnInterval = 1f; // Time interval between spawns in seconds
       [SerializeField] float m_spacing = 2f; // Minimum distance between spawned GameObjects
       [SerializeField] private int m_numberOfGameObjectsToSpawn = 10; // Total number of GameObjects to spawn
       [SerializeField] Vector3 m_spawningDirection;
       //[SerializeField] ISpawnPositionDesignator m_SpawnPositionDesignator;

       [SerializeField] GameObjectPool m_gameObjectPool;
       //[SerializeField] Bounds m_boundary;
       [SerializeField] CapsuleCollider m_circularBoundary;
       [SerializeField] Collider2D m_spawnRegion2D;
       [SerializeField] Collider m_spawnRegion3D;
       //[SerializeField] List<WayPoints>
       [SerializeField] List<Transform> m_spawnPoints;
       [SerializeField] Transform m_startingSpawnTransform;
       [SerializeField] Transform m_directionHelper; // Helper transform to visualize the spawning direction in the editor
       */
       [Header("Transformation changes are called before OnSpawn")]
       public UnityEvent<GameObject> OnTransformChanges;
       public UnityEvent<GameObject> OnSpawned; // Event to notify when a GameObject is spawned
       public UnityEvent OnStartSpawner;
       public UnityEvent<List<GameObject>> OnFinishedSpawning;
       private int m_numOfCyclesCompleted = 0;

    private void Start()
    {
      
      if ( m_SpawnerConfig.GetGameObjectPool() == null)
      {
        Debug.LogError("No GameObjectPool assigned to spawner!");
        return;
      }

      OnStartSpawner?.Invoke(); 
      OnFinishedSpawning?.Invoke(GetActiveGameObjectsFromSpawner());


      //StartCoroutine(SpawnGameObjectInBoundaryRoutine(m_spawnInterval)); // Spawn a new GameObject every 1 second (adjust the interval as needed)      
    }

    public void SpawnGameObjectsAlongSpawnPoints()
    {
      StartCoroutine(SpawnGameObjectsAlongSpawnPointsRoutine());
    }

    private IEnumerator SpawnGameObjectsAlongSpawnPointsRoutine()
    {
    // Declare and initialize local variables, specific to the SpawnGameObjectsRoutine method scope.
      int currentIndex = 0;
      float spawnInterval = m_SpawnerConfig.GetSpawnInterval();
      float spawnCycleDelay = m_SpawnerConfig.GetSpawnCycleDelay();
      float numOfGameObjsToSpawn = m_SpawnerConfig.GetNumberOfGameObjectsToSpawn();
      bool repeatSpawn = m_SpawnerConfig.GetRepeatSpawn();
      int numOfSpawnWavesRequested = m_SpawnerConfig.GetNumOfSpawnWavesRequested();
      GameObjectPool gameObjectPool = m_SpawnerConfig.GetGameObjectPool();
      var spawnIntervalWaiter = new WaitForSeconds(spawnInterval); // WaitForSeconds object to handle the spawn interval delay between spawns.
      var spawnCycleDelayWaiter = new WaitForSeconds(spawnCycleDelay); // WaitForSeconds object to handle the delay between spawn cycles when repeat spawning is enabled.
      
    // Loop while repeat spawner is true, if repeat spawner is false, this loop will only run once, effectively making it a one time spawn of the specified number of GameObjects.
    do
    {

      // Loop to spawn the specified number of GameObjects.
      for (currentIndex = 0; currentIndex < numOfGameObjsToSpawn; currentIndex++)
      {

        // Get a GameObject from the pool and spawn it in the specified direction with the specified spacing.
        var gameObjectFromPool = gameObjectPool.Pool.Get();

        if (gameObjectFromPool != null)
        {

          // Transformation changes
          OnTransformChanges?.Invoke(gameObjectFromPool);

          // Get a random position in the region provided, check to see if there is an object there, if so find a new random spot in the region.
          gameObjectFromPool.transform.position = GetSpawnPoint(currentIndex);

          OnSpawned?.Invoke(gameObjectFromPool); // Invoke the OnSpawned event to notify subscribers that a GameObject has been spawned
        }

        // Wait for the specified spawn interval before spawning the next GameObject in the line.
        yield return spawnIntervalWaiter; // Wait for the specified spawn interval before spawning the next GameObject in the line.

      }

      // Check for the number of cycles completed, if it is equal to the number of cycles to repeat, then set repeat spawn to false to stop the spawning process. This allows for a finite number of spawn cycles if desired, otherwise it will continue indefinitely if repeat spawn is true and no cycle limit is set
      if (m_numOfCyclesCompleted == numOfSpawnWavesRequested)
      {
        repeatSpawn = false; // Set repeat spawn to false to stop the spawning process after the desired number of spawn cycles is completed.
      }

      // Wait for the delay between spawn cycles if repeat spawning is enabled before starting the next spawn cycle.
      if (repeatSpawn)
      {
        yield return spawnCycleDelayWaiter; // Wait for the specified spawn cycle delay before starting the next spawn cycle.
      }

      // Update loop variant, used to control the terminating condition.
      m_numOfCyclesCompleted++;

    } while (repeatSpawn); // Check the repeat spawn condition to determine whether to continue spawning GameObjects in a loop or not.
                             // Function return stubb

    yield return null;
  }

    private Vector3 GetSpawnPoint(int index)
    {
      List<Transform> spawnPoints = m_SpawnerConfig.GetSpawnPoints();
      if(spawnPoints.Count == 0)
      {
        Debug.LogError("No spawn points assigned to spawner!");
        return Vector3.zero;
      }
      return spawnPoints[index % spawnPoints.Count].position; // Loop through spawn points if index exceeds the list count
  }

  public void SpawnGameObjectsIn3DBoxRegion()
    {
      StartCoroutine(SpawnGameObjectsIn3DBoxRegionRoutine());
    }
    
    private IEnumerator SpawnGameObjectsIn3DBoxRegionRoutine()
    {
    // Declare and initialize local variables, specific to the SpawnGameObjectsRoutine method scope.
    int currentIndex = 0;
    float spawnInterval = m_SpawnerConfig.GetSpawnInterval();
    float spawnCycleDelay = m_SpawnerConfig.GetSpawnCycleDelay();
    float numOfGameObjsToSpawn = m_SpawnerConfig.GetNumberOfGameObjectsToSpawn();
    bool repeatSpawn = m_SpawnerConfig.GetRepeatSpawn();
    int numOfSpawnWavesRequested = m_SpawnerConfig.GetNumOfSpawnWavesRequested();
    GameObjectPool gameObjectPool = m_SpawnerConfig.GetGameObjectPool();
    Collider spawnRegion3D = m_SpawnerConfig.GetSpawnRegion3D();
    var spawnIntervalWaiter = new WaitForSeconds(spawnInterval); // WaitForSeconds object to handle the spawn interval delay between spawns.
    var spawnCycleDelayWaiter = new WaitForSeconds(spawnCycleDelay); // WaitForSeconds object to handle the delay between spawn cycles when repeat spawning is enabled.
   
    int maxNumOfAttempts = 25;
      // Loop while repeat spawner is true, if repeat spawner is false, this loop will only run once, effectively making it a one time spawn of the specified number of GameObjects.
      do
      {

        // Loop to spawn the specified number of GameObjects.
        for (currentIndex = 0; currentIndex < numOfGameObjsToSpawn; currentIndex++)
        {

          // Get a GameObject from the pool and spawn it in the specified direction with the specified spacing.
          var gameObjectFromPool = gameObjectPool.Pool.Get();

          if (gameObjectFromPool != null)
          {

            // Transformation changes
            OnTransformChanges?.Invoke(gameObjectFromPool);

            // Get a random position in the region provided, check to see if there is an object there, if so find a new random spot in the region.
            gameObjectFromPool.transform.position = GetSpawnPositionForObjFromSpawnRegion3D(gameObjectFromPool, spawnRegion3D, maxNumOfAttempts);

            OnSpawned?.Invoke(gameObjectFromPool); // Invoke the OnSpawned event to notify subscribers that a GameObject has been spawned
          }

          // Wait for the specified spawn interval before spawning the next GameObject in the line.
          yield return spawnIntervalWaiter; // Wait for the specified spawn interval before spawning the next GameObject in the line.

        }

        // Check for the number of cycles completed, if it is equal to the number of cycles to repeat, then set repeat spawn to false to stop the spawning process. This allows for a finite number of spawn cycles if desired, otherwise it will continue indefinitely if repeat spawn is true and no cycle limit is set
        if (m_numOfCyclesCompleted == numOfSpawnWavesRequested)
        {
          repeatSpawn = false; // Set repeat spawn to false to stop the spawning process after the desired number of spawn cycles is completed.
        }

        // Wait for the delay between spawn cycles if repeat spawning is enabled before starting the next spawn cycle.
        if (repeatSpawn)
        {
          yield return spawnCycleDelayWaiter; // Wait for the specified spawn cycle delay before starting the next spawn cycle.
        }

        // Update loop variant, used to control the terminating condition.
        m_numOfCyclesCompleted++;

      } while (repeatSpawn); // Check the repeat spawn condition to determine whether to continue spawning GameObjects in a loop or not.
                               // Function return stubb

      yield return null;
    }

    private Vector3 GetSpawnPositionForObjFromSpawnRegion3D(GameObject gameObj, Collider spawnRegion, int maxAttempts)
    {
      // Declare and initialize variables
      Bounds spawnRegionBounds = spawnRegion.bounds;

      Bounds gameObjBounds = gameObj.GetComponent<Collider>().bounds;
      Vector3 spawnPosition = spawnRegionBounds.center;
      LayerMask mask = 1 << gameObj.layer;
      int numOfAttempts = 0;


      do
      {
        // Get a random position from the spawn region
        spawnPosition = GetRandomPositionFromRegion3D(spawnRegionBounds);
        numOfAttempts += 1;

      } while (!CheckIfPositionIsFree3D(gameObj, spawnPosition, mask) && numOfAttempts < maxAttempts);

      // Function stubb 
      return spawnPosition;
  }

    private Vector3 GetRandomPositionFromRegion3D(Bounds spawnRegionBounds)
  {
    // Declare and initialize variables
    // Declare and initialize variables
    Vector3 randomPosition = Vector3.zero;

    Vector3 center = spawnRegionBounds.center;
    Vector3 halfSize = spawnRegionBounds.extents;

    float minX = center.x - halfSize.x;
    float maxX = center.x + halfSize.x;

    float minY = center.y - halfSize.y;
    float maxY = center.y + halfSize.y;

    float minZ = center.z - halfSize.z;
    float maxZ = center.z + halfSize.z;

    float xPos = UnityEngine.Random.Range(minX, maxX);
    float yPos = UnityEngine.Random.Range(minY, maxY);
    float zPos = UnityEngine.Random.Range(minZ, maxZ);

    randomPosition = new Vector3(xPos, yPos, zPos);

    Debug.Log("Random position sent: " + randomPosition);

    // Function stub
    return randomPosition;
  }

    public void SpawnGameObjectsInARegion2D()
    {
      Collider2D spawnRegion2D = m_SpawnerConfig.GetSpawnRegion2D();
      if (spawnRegion2D != null)
      {
        StartCoroutine(SpawnGameObjectsInARegion2DRoutine(spawnRegion2D));
      }
      else
      {
        Debug.LogError("Function, SpawnGameObjectsInARegion(), Spawner needs a region in order to spawn in an region ~line: 50 Spawner.cs");
      }
    }

    private IEnumerator SpawnGameObjectsInARegion2DRoutine(Collider2D collider)
    {
    // Declare and initialize local variables, specific to the SpawnGameObjectsRoutine method scope.
    Transform startingSpawnTransform = m_SpawnerConfig.GetStartingSpawnTransform();
    Vector3 startingPosition = startingSpawnTransform.position; // Starting position for spawning GameObjects, set in the Unity Editor
    int currentIndex = 0;
    float spawnInterval = m_SpawnerConfig.GetSpawnInterval();
    float spawnCycleDelay = m_SpawnerConfig.GetSpawnCycleDelay();
    float numOfGameObjsToSpawn = m_SpawnerConfig.GetNumberOfGameObjectsToSpawn();
    bool repeatSpawn = m_SpawnerConfig.GetRepeatSpawn();
    int numOfSpawnWavesRequested = m_SpawnerConfig.GetNumOfSpawnWavesRequested();
    GameObjectPool gameObjectPool = m_SpawnerConfig.GetGameObjectPool();
    var spawnIntervalWaiter = new WaitForSeconds(spawnInterval); // WaitForSeconds object to handle the spawn interval delay between spawns.
    var spawnCycleDelayWaiter = new WaitForSeconds(spawnCycleDelay); // WaitForSeconds object to handle the delay between spawn cycles when repeat spawning is enabled.

    int maxNumOfAttempts = 25;
    // Loop while repeat spawner is true, if repeat spawner is false, this loop will only run once, effectively making it a one time spawn of the specified number of GameObjects.
    do
    {

      // Loop to spawn the specified number of GameObjects.
      for (currentIndex = 0; currentIndex < numOfGameObjsToSpawn; currentIndex++)
      {

        // Get a GameObject from the pool and spawn it in the specified direction with the specified spacing.
        var gameObjectFromPool = gameObjectPool.Pool.Get();

        if (gameObjectFromPool != null)
        {

          // Transformation changes
          OnTransformChanges?.Invoke(gameObjectFromPool);

          // Get a random position in the region provided, check to see if there is an object there, if so find a new random spot in the region.
          gameObjectFromPool.transform.position = GetSpawnPositionForObjFromSpawnRegion2D(gameObjectFromPool, collider, maxNumOfAttempts);

          OnSpawned?.Invoke(gameObjectFromPool); // Invoke the OnSpawned event to notify subscribers that a GameObject has been spawned
        }

        // Wait for the specified spawn interval before spawning the next GameObject in the line.
        yield return spawnIntervalWaiter; // Wait for the specified spawn interval before spawning the next GameObject in the line.

      }

      // Check for the number of cycles completed, if it is equal to the number of cycles to repeat, then set repeat spawn to false to stop the spawning process. This allows for a finite number of spawn cycles if desired, otherwise it will continue indefinitely if repeat spawn is true and no cycle limit is set
      if (m_numOfCyclesCompleted == numOfSpawnWavesRequested)
      {
        repeatSpawn = false; // Set repeat spawn to false to stop the spawning process after the desired number of spawn cycles is completed.
      }

      // Wait for the delay between spawn cycles if repeat spawning is enabled before starting the next spawn cycle.
      if (repeatSpawn)
      {
        yield return spawnCycleDelayWaiter; // Wait for the specified spawn cycle delay before starting the next spawn cycle.
      }

      // Update loop variant, used to control the terminating condition.
      m_numOfCyclesCompleted++;

    } while (repeatSpawn); // Check the repeat spawn condition to determine whether to continue spawning GameObjects in a loop or not.
                             // Function return stubb

    yield return null;
  }

    public void SpawnObjectsInALine()
    {
      float spacing = m_SpawnerConfig.GetSpacing();
      Transform directionHelper = m_SpawnerConfig.GetDirectionHelper();
      Transform startingSpawnTransform = m_SpawnerConfig.GetStartingSpawnTransform();
      Vector3 spawningDirection = directionHelper.position - startingSpawnTransform.position; // Calculate the spawning direction based on the helper transform's position relative to the starting spawn transform's position
      spawningDirection = spawningDirection.normalized; // Normalize the spawning direction to ensure consistent spacing between spawned GameObjects                                                                                            
      StartCoroutine(SpawnGameObjectsInALineRoutine(spawningDirection, spacing));
    }

    public void SpawnGameObjectsInLine(Vector3 spawningDir, float spacing, int numOfObjectsToSpawn)
    {

      m_SpawnerConfig.SetNumberOfGameObjectsToSpawn(numOfObjectsToSpawn); // Update the number of GameObjects to spawn based on the method parameter
      StartCoroutine(SpawnGameObjectsInALineRoutine(spawningDir, spacing));
    }

    private IEnumerator SpawnGameObjectsInALineRoutine(Vector3 spawningDirection, float distanceBetweenGameObjects)
    {
      // Declare and initialize local variables, specific to the SpawnGameObjectsRoutine method scope.
      Transform startingSpawnTransform = m_SpawnerConfig.GetStartingSpawnTransform();
      Vector3 startingPosition = startingSpawnTransform.position; // Starting position for spawning GameObjects, set in the Unity Editor
      int currentIndex = 0;
      float spawnInterval = m_SpawnerConfig.GetSpawnInterval();
      float spawnCycleDelay = m_SpawnerConfig.GetSpawnCycleDelay();
      float numOfGameObjsToSpawn = m_SpawnerConfig.GetNumberOfGameObjectsToSpawn();
      bool repeatSpawn = m_SpawnerConfig.GetRepeatSpawn();
      int numOfSpawnWavesRequested = m_SpawnerConfig.GetNumOfSpawnWavesRequested();
      GameObjectPool gameObjectPool = m_SpawnerConfig.GetGameObjectPool();
      var spawnIntervalWaiter = new WaitForSeconds(spawnInterval); // WaitForSeconds object to handle the spawn interval delay between spawns.
      var spawnCycleDelayWaiter = new WaitForSeconds(spawnCycleDelay); // WaitForSeconds object to handle the delay between spawn cycles when repeat spawning is enabled.

    // Loop while repeat spawner is true, if repeat spawner is false, this loop will only run once, effectively making it a one time spawn of the specified number of GameObjects.
    do
    {

      // Loop to spawn the specified number of GameObjects.
      for (currentIndex = 0; currentIndex < numOfGameObjsToSpawn; currentIndex++)
      {

        // Get a GameObject from the pool and spawn it in the specified direction with the specified spacing.
        var gameObjectFromPool = gameObjectPool.Pool.Get();

        if (gameObjectFromPool != null)
        {
          //gameObjectFromPool.transform.position = GetPositionFromSpawnPositionDesignator();
          OnTransformChanges?.Invoke(gameObjectFromPool);
          gameObjectFromPool.transform.position = startingPosition + (spawningDirection * distanceBetweenGameObjects * currentIndex);
          OnSpawned?.Invoke(gameObjectFromPool); // Invoke the OnSpawned event to notify subscribers that a GameObject has been spawned
        }

        // Wait for the specified spawn interval before spawning the next GameObject in the line.
        yield return spawnIntervalWaiter; // Wait for the specified spawn interval before spawning the next GameObject in the line.

      }
      // Update loop variant, used to control the terminating condition.
      m_numOfCyclesCompleted++;

      // Check for the number of cycles completed, if it is equal to the number of cycles to repeat, then set repeat spawn to false to stop the spawning process. This allows for a finite number of spawn cycles if desired, otherwise it will continue indefinitely if repeat spawn is true and no cycle limit is set
      if(m_numOfCyclesCompleted == numOfSpawnWavesRequested)
      {
        repeatSpawn = false; // Set repeat spawn to false to stop the spawning process after the desired number of spawn cycles is completed.
      }

      // Wait for the delay between spawn cycles if repeat spawning is enabled before starting the next spawn cycle.
      if (repeatSpawn)
      {
        yield return spawnCycleDelayWaiter; // Wait for the specified spawn cycle delay before starting the next spawn cycle.
      }


    } while (repeatSpawn); // Check the repeat spawn condition to determine whether to continue spawning GameObjects in a loop or not.
                             // Function return stubb
    
      yield return null;
    }

    private Vector3 GetSpawnPositionForObjFromSpawnRegion2D(GameObject gameObj, Collider2D spawnRegion, int maxAttempts)
    {

      // Declare and initialize variables
      Bounds spawnRegionBounds = spawnRegion.bounds;
     
      Bounds gameObjBounds = gameObj.GetComponent<Collider2D>().bounds;
      Vector3 spawnPosition = spawnRegionBounds.center;
      LayerMask mask =  1 << gameObj.layer;
      int numOfAttempts = 0;


     do
     {
        // Get a random position from the spawn region
        spawnPosition = GetRandomPositionFromRegion2D(spawnRegionBounds);
        numOfAttempts += 1;

      } while (!CheckIfPositionIsFree2D(gameObj, spawnPosition, mask) && numOfAttempts < maxAttempts);

      // Function stubb 
      return spawnPosition;
    }

    private bool CheckIfPositionIsFree2D(GameObject gameObj, Vector2 position, LayerMask mask)
    {
       // Declare and initialize variables
       Collider2D collider2D = gameObj.GetComponent<Collider2D>();
       Bounds size = collider2D.bounds;
       Vector2 halfSize = size.extents;
       Vector2 offset = size.center - collider2D.transform.position;

      if (collider2D != null) 
      {

            Collider2D hit = Physics2D.OverlapBox(
            position + offset,
            halfSize * 2f,
            0f,
            mask
        );

        // Return true if the hit is overlapping an object. 
        if( hit == null)
        {
          return true;
        }
        else
        {
          return false;
        }

      }
      else 
      {
        Debug.LogError("Spawner is in unable to spawn correctly no collider2d detected.");
        // Function stubb
        return false;
      }


    }

    private bool CheckIfPositionIsFree3D(GameObject gameObj, Vector3 position, LayerMask mask)
    {
      if (!gameObj.TryGetComponent(out Collider collider))
      {
        Debug.LogError("Spawner unable to spawn correctly — no Collider detected.");
        return false;
      }

      Bounds bounds = collider.bounds;

      Vector3 halfSize = bounds.extents;
      Vector3 offset = bounds.center - collider.transform.position;

      Collider[] hits = Physics.OverlapBox(
          position + offset,
          halfSize,
          collider.transform.rotation,
          mask
      );

      return hits.Length == 0; // true = space is free


  }
    private Vector3 GetRandomPositionFromRegion2D(Bounds bounds)
    {

       // Declare and initialize variables
       Vector3 randomPosition = Vector3.zero;
       Vector2 center = bounds.center;
       Vector2 halfSize = bounds.extents;
       float minX = center.x - halfSize.x;
       float maxX = center.x + halfSize.x;
       float minY = center.y - halfSize.y;
       float maxY = center.y + halfSize.y;
       float xPos = UnityEngine.Random.Range( minX, maxX );
       float yPos = UnityEngine.Random.Range( minY, maxY );
 
       randomPosition = new Vector3(xPos, yPos, 0);
       
       

       // Function stubb
       return randomPosition;

    }

    private IEnumerator SpawnGameObjectInBoundaryRoutine(float spawnInterval)
    {
       var waiter = new WaitForSeconds(spawnInterval);
       GameObjectPool gameObjectPool = m_SpawnerConfig.GetGameObjectPool(); 
       while(true)
       {
         var gameObjectFromPool = gameObjectPool.Pool.Get();
         if (gameObjectFromPool != null) 
         { 
           SpawnGameObjectInBoundary(gameObjectFromPool);
         }
         yield return waiter;
      }

    }

    /*
    private void Update()
    {
      // Declare and initialize Update method local scope variables.

      // Check for mouse input and if the left mouse button is being pressed down, then call the
      // Spawn method on the GameObjectPool instance to spawn a new GameObject from the pool.
      if (Input.GetKeyDown(KeyCode.Space))
      {
        var gameObjectFromPool = m_gameObjectPool.Pool.Get();
        SpawnGameObjectInBoundary(gameObjectFromPool);
      }
    }*/

    public List<GameObject> GetActiveGameObjectsFromSpawner()
    {
       GameObjectPool gameObjectPool = m_SpawnerConfig.GetGameObjectPool();
       return gameObjectPool.GetActiveObjectsInPool();
    }

    public void SpawnGameObjectInBoundary(GameObject gameObjectFromPool)
    {
      // Declare and initialize variables.

      gameObjectFromPool.transform.position = GetSpawnPositionForObjectInCapsule3DBoundary(gameObjectFromPool);
      
   
    }

    private Vector3 GetSpawnPositionForObjectInCapsule3DBoundary(GameObject gameObjectFromPool)
    {
      // Declare and initialize variables.
      CapsuleCollider circularBoundary = gameObjectFromPool.GetComponent<CapsuleCollider>();
      var radius = circularBoundary.radius;
      var center = circularBoundary.bounds.center;
      float xPosition = UnityEngine.Random.Range(center.x-radius,center.x+radius);
      float zPosition = UnityEngine.Random.Range(center.z-radius,center.z+radius);
      var max = circularBoundary.bounds.max;
      var yPosition = center.y;


      return new Vector3(xPosition,yPosition,zPosition);
    }

    private void OnDrawGizmos()
    {
      // Declare and initialize variables.
      Transform directionHelper = m_SpawnerConfig.GetDirectionHelper();
      Transform startingSpawnTransform = m_SpawnerConfig.GetStartingSpawnTransform();

      if(directionHelper != null && startingSpawnTransform != null)
      {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startingSpawnTransform.position, directionHelper.position);
      }
    }

  }

