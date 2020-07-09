using UnityEngine;

namespace Geekbrains
{
    public static class CustumDebug
    {
        public static bool IsDebug;
        public static void Log(object value)
        {
#if UNITY_EDITOR_WIN             // <----- added
            //if (IsDebug)       // <----- changed
            {
                Debug.Log(value);
            }
#endif                          // <----- added
        }
    }
}
