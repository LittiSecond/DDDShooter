using UnityEngine;
using System.Collections;

using Geekbrains;


namespace DddShooter
{
    public sealed class GameStarter : MonoBehaviour
    {
        #region Fields

        private SceneManagerHelper _sceneManager;

        #endregion


        #region UnityMethods

        private void Start()
        {
            //Debug.Log("GameStarter::Start: ");
            DontDestroyOnLoad(gameObject);
            _sceneManager = GetComponent<SceneManagerHelper>();
            _sceneManager.LoadSceneAsync(SceneManagerHelper.Instance.Scenes.MainMenuScene.SceneAsset.name);
        }

        #endregion

    }
}