using System.IO;
using System.Collections.Generic;
using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class SaveLoadManager
    {

        #region Fields

        private IData<SerializableGameObject> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.txt";
        private string _path;

        #endregion


        #region Properties

        public SaveDataFormat DataFormat { get; set; }

        #endregion


        #region ClassLifeCycles

        public SaveLoadManager(IData<SerializableGameObject> data)
        {
            _data = data;
            _path = Path.Combine(Application.dataPath, _folderName);
        }

        #endregion


        #region Methods

        public void Save(SerializableGameObject obj)
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            _data.SetPath(Path.Combine(_path, _fileName));
            _data.Save(obj);
        }

        #endregion


    }
}
