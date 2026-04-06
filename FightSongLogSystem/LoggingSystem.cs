using Unity.VisualScripting;
using UnityEngine;

namespace FightSongLoggingSystem
{
    public static class LoggingSystem
    {

       public static bool IsConsoleLogging = true;
       public static bool IsUILogging = false;
       public static bool IsLoggingToFile = false;

       

       public static void LogString(string message, WarningLevel warningLevel, params object[] args)
       {
          #if FIGHTSONG_LOGGING

            string formattedMessage = string.Format(message, args);

            if (IsConsoleLogging)
            {
              switch (warningLevel)
              {
                case WarningLevel.Info:
                  Debug.Log(formattedMessage);
                  break;

                case WarningLevel.Caution:
                  Debug.LogWarning(formattedMessage);
                  break;

                case WarningLevel.Severe:
                case WarningLevel.Error:
                  Debug.LogError(formattedMessage);
                  break;
              }
            }

          #endif

          // Push to UI, if UI logging is requested.

          // Push to history file, if requested.
          if (IsLoggingToFile)
          {
                  // Check for args for file name. 

                  // If args has no file name then write to default log.


          }
       }

    }
}
