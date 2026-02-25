using System.Collections.Generic;
using UnityEngine;

namespace GameDevTV
{
    [System.Serializable]
    public struct SpawnerConfig
    {

    [SerializeField] private bool m_repeatSpawn; // Whether to repeat the spawning process after the initial spawn.

    [SerializeField, Range(1, 100)]
    private int m_numOfSpawnWavesRequested;

    [SerializeField] private bool m_randomizeSpawnDelay;
    [SerializeField] private bool m_randomizeSpawnCycleDelay;

    [SerializeField] private float m_SpawnCycleDelay; // Time delay between spawn cycles if repeatSpawn is true.

    [SerializeField, Range(0.0f, 3f)]
    private float m_spawnInterval; // Time interval between spawns in seconds

    [SerializeField] private float m_spacing; // Minimum distance between spawned GameObjects

    [SerializeField] private int m_numberOfGameObjectsToSpawn; // Total number of GameObjects to spawn

    [SerializeField] private Vector3 m_spawningDirection;

    //[SerializeField] ISpawnPositionDesignator m_SpawnPositionDesignator;

    [SerializeField] private GameObjectPool m_gameObjectPool;

    //[SerializeField] Bounds m_boundary;

    [SerializeField] private CapsuleCollider m_circularBoundary;
    [SerializeField] private Collider2D m_spawnRegion2D;
    [SerializeField] private Collider m_spawnRegion3D;

    //[SerializeField] List<WayPoints>

    [SerializeField] private List<Transform> m_spawnPoints;
    [SerializeField] private Transform m_startingSpawnTransform;
    [SerializeField] private Transform m_directionHelper;

    // Repeat Spawn
    public bool GetRepeatSpawn() => m_repeatSpawn;
    public void SetRepeatSpawn(bool value) => m_repeatSpawn = value;


    // Spawn Waves Requested
    public int GetNumOfSpawnWavesRequested() => m_numOfSpawnWavesRequested;
    public void SetNumOfSpawnWavesRequested(int value) => m_numOfSpawnWavesRequested = value;


    // Randomization Flags
    public bool GetRandomizeSpawnDelay() => m_randomizeSpawnDelay;
    public void SetRandomizeSpawnDelay(bool value) => m_randomizeSpawnDelay = value;

    public bool GetRandomizeSpawnCycleDelay() => m_randomizeSpawnCycleDelay;
    public void SetRandomizeSpawnCycleDelay(bool value) => m_randomizeSpawnCycleDelay = value;


    // Spawn Cycle Delay
    public float GetSpawnCycleDelay() => m_SpawnCycleDelay;
    public void SetSpawnCycleDelay(float value) => m_SpawnCycleDelay = value;


    // Spawn Interval
    public float GetSpawnInterval() => m_spawnInterval;
    public void SetSpawnInterval(float value) => m_spawnInterval = value;


    // Spacing
    public float GetSpacing() => m_spacing;
    public void SetSpacing(float value) => m_spacing = value;


    // Number of Objects
    public int GetNumberOfGameObjectsToSpawn() => m_numberOfGameObjectsToSpawn;
    public void SetNumberOfGameObjectsToSpawn(int value) => m_numberOfGameObjectsToSpawn = value;


    // Spawning Direction
    public Vector3 GetSpawningDirection() => m_spawningDirection;
    public void SetSpawningDirection(Vector3 value) => m_spawningDirection = value;


    // GameObject Pool
    public GameObjectPool GetGameObjectPool() => m_gameObjectPool;
    public void SetGameObjectPool(GameObjectPool value) => m_gameObjectPool = value;


    // Boundaries / Regions
    public CapsuleCollider GetCircularBoundary() => m_circularBoundary;
    public void SetCircularBoundary(CapsuleCollider value) => m_circularBoundary = value;

    public Collider2D GetSpawnRegion2D() => m_spawnRegion2D;
    public void SetSpawnRegion2D(Collider2D value) => m_spawnRegion2D = value;

    public Collider GetSpawnRegion3D() => m_spawnRegion3D;
    public void SetSpawnRegion3D(Collider value) => m_spawnRegion3D = value;


    // Spawn Points
    public List<Transform> GetSpawnPoints() => m_spawnPoints;
    public void SetSpawnPoints(List<Transform> value) => m_spawnPoints = value;


    // Starting Transform
    public Transform GetStartingSpawnTransform() => m_startingSpawnTransform;
    public void SetStartingSpawnTransform(Transform value) => m_startingSpawnTransform = value;


    // Direction Helper
    public Transform GetDirectionHelper() => m_directionHelper;
    public void SetDirectionHelper(Transform value) => m_directionHelper = value;

  }
}
