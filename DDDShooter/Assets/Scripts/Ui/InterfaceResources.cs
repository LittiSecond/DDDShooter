using UnityEngine;


namespace DddShooter
{
    public class InterfaceResources : MonoBehaviour
    {

        #region Fields

        private UiProgressBar _uiProgressBar;

        #endregion


        #region Properties

        public Canvas MainCanvas { get; private set; }

        public UiProgressBar ProgressBar 
        {
            get
            {
                if (!_uiProgressBar)
                {
                    GameObject go = ResourcesManager.GetPrefab(PrefabId.ProgressBar);
                    GameObject goInstance = Instantiate(go, MainCanvas.transform);
                    _uiProgressBar = goInstance.GetComponent<UiProgressBar>();
                    _uiProgressBar.SetActive(false);
                }
                return _uiProgressBar;
            }
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            MainCanvas = FindObjectOfType<Canvas>();
        }

        #endregion

    }
}