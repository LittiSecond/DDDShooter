using UnityEngine;
using UnityEditor;

using DddShooter.Test;

namespace DddShooter.Editor
{
    [CustomEditor(typeof(StrangeObjectScene))]
    public class StrangeObjectSceneEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Расширения редактора:");
            bool isPress = GUILayout.Button("Подсчитать объекты", EditorStyles.miniButtonLeft);

        }

    }
}
