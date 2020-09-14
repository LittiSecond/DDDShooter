using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Geekbrains
{
    internal class SceneManagerHelper : Singleton<SceneManagerHelper>
    {
        #region Fields

        public Scenes Scenes;

        #endregion


        #region LoadScene
        public void LoadSceneAsync(int lvl)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(lvl);
            StartCoroutine(LoadSceneAsync(async));
        }
        public void LoadSceneAsync(Scene lvl)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(lvl.buildIndex);
            StartCoroutine(LoadSceneAsync(async));
        }
        public void LoadSceneAsync(string lvl)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(lvl);
            StartCoroutine(LoadSceneAsync(async));
        }

        private IEnumerator LoadSceneAsync(AsyncOperation async)
        {
            ProgressBarEnabled();
            async.allowSceneActivation = false;
            while (!async.isDone)
            {
                ProgressBarSetValue(async.progress + 0.1f);
                float progress = async.progress * 100f;
                if (async.progress < 0.9f && Mathf.RoundToInt(progress) != 100)
                {
                    async.allowSceneActivation = false;
                }
                else
                {
                    if (async.allowSceneActivation) yield return null;
                    async.allowSceneActivation = true;
                    ProgressBarDisable();
                }
                yield return null;
            }
        }

        private void ProgressBarEnabled()
        {

        }

        private void ProgressBarSetValue(float value)
        {

        }

        private void ProgressBarDisable()
        {

        }

        #endregion
    }
}