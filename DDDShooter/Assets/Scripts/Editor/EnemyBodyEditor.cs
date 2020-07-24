using System;
using UnityEngine;
using UnityEditor;

namespace DddShooter.Editor
{
    [CustomEditor(typeof(EnemyBody))]
    public class EnemyBodyEditor : UnityEditor.Editor
    {
        #region Fields

        public GameObject[] _weaponPrefabs;
        public GameObject _weaponPrefab;

        #endregion


        #region UnityMethods

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EnemyBody enemyBody = (EnemyBody)target;

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Расширения редактора:");

            _weaponPrefab = EditorGUILayout.ObjectField("Объект", _weaponPrefab, typeof(GameObject), 
                false) as GameObject ;

            //_weaponPrefabs = EditorGUILayout.ObjectField("Объект", _weaponPrefabs, typeof(GameObject[]),
            //    false) as GameObject[];

        }

        #endregion

    }
}