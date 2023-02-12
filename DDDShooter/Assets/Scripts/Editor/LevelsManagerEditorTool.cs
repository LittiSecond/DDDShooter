using UnityEngine;
using UnityEditor;

namespace LiftSimulator.Editor
{
    public class LevelsManagerEditorTool : EditorWindow
    {
        #region Fields


        private string _text;
        private string _text2;

        #endregion


        #region UnityMethods

        private void OnGUI()
        {
            GUILayout.Space(20);

            _text = EditorGUILayout.TextArea(_text, GUILayout.Height(100));
            _text2 = EditorGUILayout.TextField(_text2);
        }

        #endregion


        #region Methods

        [MenuItem("CustomTools/LevelsManager")]
        static void ShowLevelsManager()
        {
            EditorWindow.GetWindow(typeof(LevelsManagerEditorTool));
        }

        #endregion

    }
}