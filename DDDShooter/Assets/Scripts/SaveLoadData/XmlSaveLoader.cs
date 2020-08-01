using System;
using System.IO;
using System.Xml.Serialization;

using Geekbrains;


namespace DddShooter
{
    public sealed class XmlSaveLoader : IData<SerializableGameObject>
    {
        #region Fields

        private static XmlSerializer _formatter;
        
        private string _path;

        private const string EXTENSION = ".xml";

        #endregion


        #region ClassLifeCycles

        public XmlSaveLoader()
        {
            _formatter = new XmlSerializer(typeof(SerializableGameObject));
        }

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

            CustumDebug.Log("XmlSaveLoader->Save: _path = " + _path);
            if (_path != null)
            {
                using (FileStream fs = new FileStream(_path, FileMode.Create))
                {
                    _formatter.Serialize(fs, data);
                }
            }
        }

        public void SetPath(string path)
        {
            _path = Path.ChangeExtension(path, EXTENSION);
        }

        #endregion

    }
}
