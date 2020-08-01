using System;
using System.IO;
using Geekbrains;


namespace DddShooter
{
    public sealed class StreamSaveLoader : IData<SerializableGameObject>
    {
        #region Fields

        private string _path;

        private const string EXTENSION = ".txt";

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
             
            CustumDebug.Log("StreamSaveLoader->Save: _path = " + _path);
            if (_path != null)
            {
                using (var sw = new StreamWriter(_path))
                {
                    sw.WriteLine(data.Name);
                    sw.WriteLine(data.Pos.ToString());
                    sw.WriteLine(data.Rot.ToString());
                    sw.WriteLine(data.Scale.ToString());
                    sw.WriteLine(data.IsEnable);
                    //sw.Flush();
                    //sw.Close();
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
