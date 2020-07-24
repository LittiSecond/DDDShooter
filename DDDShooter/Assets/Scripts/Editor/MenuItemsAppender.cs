using UnityEngine;
using UnityEditor;


namespace DddShooter.Editor
{
    public class MenuItemsAppender //: ScriptableObject
    {
        //[MenuItem("Tools/MyTool/Do It in C#")]
        //static void DoIt()
        //{
        //    EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        //}

        [MenuItem("DddTools/Бесполезне пункты/Не нужный пункт 1")]
        public static void UselesItem1()
        {
            Debug.Log("MenuItemsAppender->UselesItem1: ");
        }

        [MenuItem("DddTools/Бесполезне пункты/Не нужный пункт 2")]
        public static void UselesItem2()
        {
            Debug.Log("MenuItemsAppender->UselesItem2: ");
        }

        [MenuItem("DddTools/Бесполезне пункты/Узнать пути")]
        public static void Paths()
        {
            string dataPath = Application.dataPath;             // Содержит путь к папке игровых данных
            string persistentDataPath = Application.persistentDataPath;   // Путь к постоянной директории данных
            string streamingAssetsPath = Application.streamingAssetsPath;  // Путь к папке StreamingAssets
            string temporaryCachePath = Application.temporaryCachePath;   // Путь к временным данным/директории кэша

            Debug.Log("MenuItemsAppender->Paths: dataPath = " + dataPath);
            Debug.Log("MenuItemsAppender->Paths: persistentDataPath = " + persistentDataPath);
            Debug.Log("MenuItemsAppender->Paths: streamingAssetsPath = " + streamingAssetsPath);
            Debug.Log("MenuItemsAppender->Paths: temporaryCachePath = " + temporaryCachePath);
        }

        [MenuItem("CONTEXT/EnemyBody/пункт")]
        public static void EnemyBodyContext()
        {
            Debug.Log("MenuItemsAppender->EnemyBodyContext: ");
        }

    }
}