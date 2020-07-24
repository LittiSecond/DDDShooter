using System;
using System.IO;
using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class JsonSaveLoader : IData<SerializableGameObject>
    {
        #region Fields

        private string _path;

        private const string EXTENSION = ".json";

        #endregion


        #region IData

        public SerializableGameObject Load(string path = null)
        {
            if (path != null)
            {
                _path = Path.ChangeExtension(path, EXTENSION);
            }
            SerializableGameObject result = new SerializableGameObject();

            if (_path != null)
            {

            }
            return result;
        }

        public void Save(SerializableGameObject data, string path = null)
        {
            if (path != null)
            {
                _path = Path.ChangeExtension(path, EXTENSION);
            }

            CustumDebug.Log("JsonSaveLoader->Save: _path = " + _path);
            if (_path != null)
            {
                string str = JsonUtility.ToJson(data);
                File.WriteAllText(_path, str);
            }
        }

        public void SetPath(string path)
        {
            _path = Path.ChangeExtension(path, EXTENSION);
        }

        #endregion
    }
}
