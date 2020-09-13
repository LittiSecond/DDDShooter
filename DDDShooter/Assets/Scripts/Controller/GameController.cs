using UnityEngine;

namespace Geekbrains
{
    public sealed class GameController : MonoBehaviour
    {
        private Controllers _controllers;
        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
            DontDestroyOnLoad(gameObject);
        }
        
        private void Update()
        {
            for (var i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }
        }
        
        public void ExitProgramm()
        {
//#if UNITY_EDITOR_WIN 
#if UNITY_EDITOR 
            Debug.Log("GameController->ExitProgramm: exit programm");
            //UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
