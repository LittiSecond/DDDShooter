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

    }
}