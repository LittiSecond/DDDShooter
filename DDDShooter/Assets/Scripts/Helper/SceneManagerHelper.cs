using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Geekbrains;


namespace DddShooter
{
    internal class SceneManagerHelper : Singleton<SceneManagerHelper>
    {
        #region Fields

        public Scenes Scenes;

        private UiProgressBar _progressBar;
        private bool _haveProgressBar;

        #endregion


        //private void Start()
        //{
        //    InterfaceResources ifr = FindObjectOfType<InterfaceResources>();
        //    _progressBar = ifr.GetComponent<SliderUI>();
        //}


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
            _haveProgressBar = false;
            InterfaceResources ifr = FindObjectOfType<InterfaceResources>();
            if (ifr)
            {
                _progressBar = ifr.ProgressBar;
                _haveProgressBar = _progressBar != null;
            }

            if (_haveProgressBar)
            {
                _progressBar.SetActive(true);
                ProgressBarSetValue(0.0f);
            }
        }

        private void ProgressBarSetValue(float value)
        {
            if (_haveProgressBar)
            {
                _progressBar.GetControl.value = value;
                _progressBar.GetText.text = string.Format("{0}%", System.Math.Truncate(value * 100));
            }
        }

        private void ProgressBarDisable()
        {
            if (_haveProgressBar)
            {
                _progressBar.SetActive(false);
            }    
        }

        #endregion
    }
}