using System.Collections.Generic;
using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class PatrolPath : MonoBehaviour
    {
        private List<Vector3> _path;


        #region UnityMethods

        private void Awake()
        {
            _path = new List<Vector3>();
            Transform[] transforms = GetComponentsInChildren<Transform>();
            for (int i = 1; i < transforms.Length; i++)
            {
                _path.Add(transforms[i].position);
            }

#if UNITY_EDITOR_WIN
            string message = "PatrolPath->Awake: ";
            foreach (Vector3 v in _path)
            {
                message += v.ToString() + "    ";
            }
            Debug.Log(message);
#endif
        }

        #endregion


        #region Methods

        public List<Vector3> GetPath()
        {
            return _path;
        }

        #endregion
    }
}