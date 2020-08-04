using System;
using UnityEngine;
using System.Collections.Generic;

using Geekbrains;

namespace DddShooter
{
    public static class PrefabManager
    {
        #region Fields

        private static Dictionary<PrefabId, GameObject> _prefabs = new Dictionary<PrefabId, GameObject>();
        private static readonly Dictionary<PrefabId, string> _prefabPaths = new Dictionary<PrefabId, string>
            {   
                { PrefabId.PlayerCharacter, "Prefabs/PlayerCharacter"}
            };

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
                CustumDebug.LogError("PrefabManager->GetPrefab: Ошибка - не удалось получить префаб id = " + id.ToString());
            }
#endif
            return go;
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
