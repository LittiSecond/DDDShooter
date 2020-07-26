using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

using Geekbrains;
using System;

namespace DddShooter
{
    public sealed class MiniMapController : BaseController, IExecute, IInitialization
    {
        #region PrivateData

        private struct RadarObject
        {
            public Transform OjectInWorld;
            public UiMarker MarkerAtMap;
        }

        #endregion

        #region Fields

        private UiMiniMap _uiMiniMap;
        //private Transform _radarObjectsRoot;

        private RadarObject _playerMarker;
        private List<RadarObject> _radarObjects = new List<RadarObject>();

        private Rect _worldArea;

        private int _counter;
        private int _max = 5;

        private bool _havePlayerMarker;

        #endregion


        #region ClassLifeCycles
               
        #endregion


        #region Methods

        public void AddObject(Transform objTransform)
        {
            if (objTransform)
            {
                if (!_radarObjects.Exists(ro => ro.OjectInWorld == objTransform))
                {
                    //RadarObject ro = new RadarObject();
                    //ro.OjectInWorld = objTransform;
                    //ro.MarkerAtMap = CreateMarker();
                    //_radarObjects.Add(ro);
                    _radarObjects.Add( new RadarObject {
                        OjectInWorld = objTransform, MarkerAtMap = _uiMiniMap.CreateMarker()}   );
                }
            }
        }

        public void RemoveObject(Transform objTransform)
        {
            if (objTransform)
            {
                int index = _radarObjects.FindIndex(ro => ro.OjectInWorld == objTransform);
                if (index >= 0)
                {
                    RadarObject rObj = _radarObjects[index];
                    _uiMiniMap.RemoveMarker(rObj.MarkerAtMap);
                    _radarObjects.RemoveAt(index);
                }
            }
        }

        //private UiMarker CreateMarker()
        //{
        //    return _uiMiniMap.CreateMarker();
        //}

        //private void RemoveMarker(UiMarker marker)
        //{
        //    _uiMiniMap.RemoveMarker(marker);
        //}

        public void SetPlayer(Transform transform)
        {
            if (transform)
            {
                if (!_havePlayerMarker)
                {
                    _playerMarker.OjectInWorld = transform;
                    _playerMarker.MarkerAtMap = _uiMiniMap.CreatePlayerMarker();
                    _havePlayerMarker = true;
                }
            }
            else
            {
                if (_havePlayerMarker)
                {
                    _playerMarker.OjectInWorld = null;
                    _uiMiniMap.RemoveMarker(_playerMarker.MarkerAtMap);
                    _playerMarker.MarkerAtMap = null;
                    _havePlayerMarker = false;
                }
            }
        }

        private Vector2 CalculatePosition(Vector3 position)
        {
            float x = (position.x - _worldArea.xMin) / _worldArea.width;
            float y = (position.z - _worldArea.yMin) / _worldArea.height;
            return new Vector2(x, y);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (_counter++ >= _max)
            {
                _counter = 0;

                foreach (RadarObject ro in _radarObjects)
                {
                    Vector2 minimapPos = CalculatePosition(ro.OjectInWorld.position);
                    ro.MarkerAtMap.SetPosition(minimapPos);
                }

                if (_havePlayerMarker)
                {
                    Vector2 minimapPos = CalculatePosition(_playerMarker.OjectInWorld.position);
                    _playerMarker.MarkerAtMap.SetPosition(minimapPos);

                    Vector3 rotation = -_playerMarker.OjectInWorld.rotation.eulerAngles;
                    _playerMarker.MarkerAtMap.SetAngle(rotation.y);
                }
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _uiMiniMap = ServiceLocatorMonoBehaviour.GetService<UiMiniMap>(false);
            if (_uiMiniMap)
            {
                //_radarObjectsRoot = _uiMiniMap.MarkerRoot;
                _worldArea = _uiMiniMap.WorldArea;
            }
        }

        #endregion
    }
}
