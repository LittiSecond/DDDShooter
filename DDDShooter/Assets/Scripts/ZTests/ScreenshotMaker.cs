using System;
using System.Collections;
using System.IO;

using UnityEngine;


namespace DddShooter.Test
{
    public sealed class ScreenshotMaker : MonoBehaviour
    {                     // скрипт сделан чтобы один раз сделать скриншёт. Не останется в релизе.
        #region Fields

        //[SerializeField] private Camera _camera;
        private string _path;
        private const string _folderName = "dataSave";

        private int _resolution = 5;
        //private int _layers = 5;

        private bool _isProcessed;

        #endregion


        #region UnityMethods

        private void Start()
        {
             _path = Path.Combine(Application.dataPath, _folderName);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Screenshot"))
            {
                MakeScreenshot2();
            }
        }

        #endregion


        #region Methods

        public void MakeScreenshot()
        {
            string filename = String.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);
            string fullPath = Path.Combine(_path, filename);
            ScreenCapture.CaptureScreenshot(fullPath, _resolution);
            Debug.Log("ScreenshotMaker->MakeScreenshot: fullPath = " + fullPath);
        }

        public void MakeScreenshot2()
        {
            if (!_isProcessed)
            {
                StartCoroutine(SaveScreenshot());
            }
        }

        private IEnumerator SaveScreenshot()
        {
            _isProcessed = true;
            yield return new WaitForEndOfFrame();
            //_camera.cullingMask = ~(1 << _layers);
            var sw = Screen.width;
            var sh = Screen.height;
            var sc = new Texture2D(sw, sh, TextureFormat.RGB24, false);

            sc.ReadPixels(new Rect(0, 0, sw, sh), 0, 0);

            var bytes = sc.EncodeToPNG();

            var filename = String.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);
            string fullPath2 = Path.Combine(_path, filename);
            System.IO.File.WriteAllBytes(fullPath2, bytes);
            Debug.Log("ScreenshotMaker->SaveScreenshot: fullPath2 = " + fullPath2);

            yield return new WaitForSeconds(2.3f);

            //_camera.cullingMask |= 1 << _layers;
            StopCoroutine(SaveScreenshot());
            _isProcessed = false;
        }

        #endregion
    }
}