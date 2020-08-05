using System.Collections.Generic;
using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class PatrolPath : MonoBehaviour
    {
        #region Fields

        private List<Vector3> _path = new List<Vector3>();
#if UNITY_EDITOR_WIN
        private Color _lineColor = Color.red;
#endif

        #endregion


        #region UnityMethods

        private void Awake()
        {
            //_path = new List<Vector3>();
            DeterminePath();

#if UNITY_EDITOR_WIN
            //string message = "PatrolPath->Awake: ";
            //foreach (Vector3 v in _path)
            //{
            //    message += v.ToString() + "    ";
            //}
            //Debug.Log(message);
#endif
        }

#if UNITY_EDITOR_WIN

        private void OnValidate()
        {
            DeterminePath();
            //Debug.Log("PatrolPath->OnValidate: " + gameObject.name);
        }

        private void OnDrawGizmosSelected()
        {
            DeterminePath();
            Gizmos.color = _lineColor;
            for (var i = 0; i < _path.Count; i++)
            {
                Vector3 currentNode = _path[i];
                Vector3 previousNode = Vector3.zero;
                if (i > 0)
                {
                    previousNode = _path[i - 1];
                }
                else if (i == 0 && _path.Count > 1)
                {
                    previousNode = _path[_path.Count - 1];
                }
                Gizmos.DrawLine(previousNode, currentNode);
                //Gizmos.DrawWireSphere(currentNode, 0.3f);
                //Debug.Log("PatrolPath->OnDrawGizmosSelected: " + gameObject.name);
            }
        }
#endif

        #endregion


        #region Methods

        public List<Vector3> GetPath()
        {
            return _path;
        } 

        private void DeterminePath()
        {
            _path.Clear();
            Transform[] transforms = GetComponentsInChildren<Transform>();
            for (int i = 1; i < transforms.Length; i++)
            {
                _path.Add(transforms[i].position);
            }
            //Debug.Log("PatrolPath->DeterminePath: _path.Count = " + _path.Count.ToString()); 
        }

        #endregion
    }
}