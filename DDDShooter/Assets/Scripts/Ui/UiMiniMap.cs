using UnityEngine;
using UnityEngine.UI;


namespace DddShooter
{
    public sealed class UiMiniMap : MonoBehaviour
    {
        #region Fields

        [SerializeField] private UiMarker _markerPrefab;
        [SerializeField] private UiMarker _playerMarkerPrefab;
        [SerializeField] private Rect _worldArea = new Rect();

        private Transform _markerRoot;

        #endregion

        #region Properties

        //public Transform MarkerRoot { get => _markerRoot; }

        public Rect WorldArea { get => _worldArea; }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            RawImage rawImage = GetComponentInChildren<RawImage>();
            if (rawImage)
            {
                _markerRoot = rawImage.transform;
                    }
        }

        #endregion


        #region Methods

        public UiMarker CreateMarker()    //  добавить защиту если префаб потерян
        {
            return Instantiate<UiMarker>(_markerPrefab, _markerRoot);
        }

        public UiMarker CreatePlayerMarker()
        {
            return Instantiate<UiMarker>(_playerMarkerPrefab, _markerRoot);
        }

        public void RemoveMarker(UiMarker marker)
        {
            Destroy(marker.gameObject);
        }

        #endregion
    }
}
