using System;
using UnityEngine;
using System.Collections.Generic;

using Geekbrains;
using UnityEngine.Audio;

namespace DddShooter
{
    public static class ResourcesManager
    {
        #region Fields

        private static Dictionary<PrefabId, GameObject> _prefabs = new Dictionary<PrefabId, GameObject>();
        private static readonly Dictionary<PrefabId, string> _prefabPaths = new Dictionary<PrefabId, string>
            {   
                { PrefabId.PlayerCharacter, "Prefabs/PlayerCharacter"},
                { PrefabId.ProgressBar, "Prefabs/UiProgressbar"},
            };

        private static AudioMixer _audioMixer = null;

        #endregion


        #region Methods

        public static GameObject GetPrefab(PrefabId id)
        {
            GameObject go = null;

            if (_prefabs.ContainsKey(id))
            {
                go = _prefabs[id];
            }
            else
            {
                go = LoadPrefab(id);
            }

#if UNITY_EDITOR_WIN
            if (go == null)
            {
                CustumDebug.LogError("ResourcesManager->GetPrefab: Ошибка - не удалось получить префаб id = " + id.ToString());
            }
#endif
            return go;
        }

        public static AudioMixer GetAudioMixer()
        {
            if (!_audioMixer)
            {
                _audioMixer = Resources.Load<AudioMixer>("MainAudioMixer");
            }
            return _audioMixer;
        }

        private static GameObject LoadPrefab(PrefabId id)
        {
            GameObject go = null;

            if (_prefabPaths.ContainsKey(id))
            {
                go = Resources.Load(_prefabPaths[id]) as GameObject;
                if (go)
                {
                    _prefabs.Add(id, go);
                }
            }

            return go;
        }

        #endregion
    }
}
