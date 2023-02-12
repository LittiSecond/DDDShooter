using UnityEngine;


namespace DddShooter.Test
{
    // первая проба применить ScriptableObject
    [CreateAssetMenu(fileName = "data", menuName =
    "CreateScriptableObject/OrbSettings", order = 1)]
    public class OrbSettings : ScriptableObject
    {
        public string Message;
        public Material Material;
    }
}
