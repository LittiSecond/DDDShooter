using System.Collections.Generic;

namespace DddShooter
{


    public static class TextConstants
    {
        #region public consts

        public const string UI_GROUP_ID = "Ui";
            public const string LOOK_AT_TEXT_ID = "LookAt";
            public const string PICK_UP_TEXT_ID = "E-PickUp";
            public const string TOO_FAR_TEXT_ID = "TooFar";
            public const string CANNOT_PICK_UP_WEAPON_TEXT_ID = "CanNotPickWeapon";
            public const string CLIP_IS_EMTY_TEXT_ID = "ClipIsEmpty";
            public const string CLOSE_TEXT_ID = "Close";
            public const string RESERV_TEXT_ID = "reserv";


        public const string MENU_ITEMS_GROUP_ID = "MenuItems";
            public const string CONTINUE_TEXT_ID = "Continue";
            public const string OPTIONS_TEXT_ID = "Options";
            public const string QUIT_TEXT_ID = "Quit";
            public const string NEW_GAME_TEXT_ID = "NewGame";





        #endregion


        #region Fields

        //private static Dictionary<int, string> _dictionary = new Dictionary<int, string>()
        //{
        //    { 0, "Ошибка, несуществующий textId" },
        //    { 1, "Взгляд на: "},
        //    { 2, "Пауза" },
        //    { 3, "Патроны" },
        //    { 4, "Обойма пуста. 'r' - перезарядить" },
        //    { 5, "'e' - взаимодействовать" },
        //    { 6, "Не удалось подобрать оружие" },
        //    { 7, "'e' - подобрать" },
        //    { 8, "Слишком далеко" },
        //    { 9, "MenuItems" },
        //};

        // 
        //private static Dictionary<TextId, string> _dictionary = new Dictionary<TextId, string>()
        //{
        //    { TextId.None, "Ошибка, несуществующий textId" },
        //    { TextId.LookAt, "Взгляд на: "},
        //    { TextId.Pause, "Пауза" },
        //    { TextId.Projectile, "Патроны" },
        //    { TextId.ClipEmpty, "Обойма пуста. 'r' - перезарядить" }
        //};

        #endregion


        #region Methods

        //public static string GetText(int textId)
        //{
        //    string str;
        //    if (_dictionary.ContainsKey(textId))
        //    {
        //        str = _dictionary[textId];
        //    }
        //    else
        //    {
        //        str = _dictionary[0];
        //    }
        //    return str;
        //}

        #endregion
    }
}
