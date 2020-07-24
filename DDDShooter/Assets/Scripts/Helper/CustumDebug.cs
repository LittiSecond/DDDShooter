using UnityEngine;

namespace Geekbrains
{
    public static class CustumDebug
    {
        public static bool IsDebug;
        public static void Log(object value)
        {
#if UNITY_EDITOR_WIN 
            //if (IsDebug)
            {
                Debug.Log(value);
            }
#endif 
        }

        public static void LogError(object value)
        {
#if UNITY_EDITOR_WIN 
            //if (IsDebug)
            {
                Debug.LogError(value);
            }
#endif  
        }

    }
}
