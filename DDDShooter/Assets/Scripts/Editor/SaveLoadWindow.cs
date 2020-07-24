using UnityEngine;
using UnityEditor;

using Geekbrains;


namespace DddShooter.Editor
{
    public class SaveLoadWindow : EditorWindow
    {
        #region Fields

        public static GameObject ObjectToSave;

        private SaveLoadManager _txtSaveLoadManager;
        private SaveLoadManager _xmlSaveLoadManager;
        private SaveLoadManager _jsonSaveLoadManager;

        private string[] _saveOptions =
            {
                "текстовый",
                "XML",
                "Json"
            };
        private int _saveOptionsSelectedIndex = -1;
        

        #endregion


        #region UnityMethods

        private void OnGUI()
        {
            GUILayout.Space(20);

            ObjectToSave = EditorGUILayout.ObjectField("Объект для сохранения: ",
                        ObjectToSave, typeof(GameObject), true) as GameObject;

            _saveOptionsSelectedIndex = EditorGUILayout.Popup( "Формат: " ,_saveOptionsSelectedIndex, _saveOptions);
            if (GUILayout.Button("Coхранить"))
            {
                SaveButtonClick();
            }
        }

        #endregion


        #region Methods

        [MenuItem("DddTools/Сохранить Загрузить объект")]
        public static void ShowToolWidow()
        {
            EditorWindow.GetWindow(typeof(SaveLoadWindow));
        }

        private void SaveButtonClick()
        {
            if (ObjectToSave)
            {
                SerializableGameObject sgo = new SerializableGameObject(ObjectToSave);
                switch (_saveOptionsSelectedIndex)
                {
                    case 0:
                        CreateTxtSaveLoadManager();
                        _txtSaveLoadManager.Save(sgo);
                        break;
                    case 1:
                        CreateXmlSaveLoadManager();
                        _xmlSaveLoadManager.Save(sgo);
                        break;
                    case 2:
                        CreateJsonSaveLoadManager();
                        _jsonSaveLoadManager.Save(sgo);
                        break;
                    default:
                        break;
                }
                Debug.Log("SaveLoadWindow->SaveButtonClick: _saveOptionsSelectedIndex = " +
                    _saveOptionsSelectedIndex.ToString());
            }
        }

        private void CreateTxtSaveLoadManager()
        {
            if (_txtSaveLoadManager == null)
            {
                _txtSaveLoadManager = new SaveLoadManager(new StreamSaveLoader());
            }
        }

        private void CreateXmlSaveLoadManager()
        {
            if (_xmlSaveLoadManager == null)
            {
                _xmlSaveLoadManager = new SaveLoadManager(new XmlSaveLoader());
            }
        }

        private void CreateJsonSaveLoadManager()
        {
            if (_jsonSaveLoadManager == null)
            {
                _jsonSaveLoadManager = new SaveLoadManager(new JsonSaveLoader());
            }
        }

        #endregion

    }
}
