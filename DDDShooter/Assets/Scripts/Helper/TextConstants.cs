using System.Collections.Generic;

namespace DddShooter
{
    public static class TextConstants
    {
        #region Fields

        private static Dictionary<int, string> _dictionary = new Dictionary<int, string>()
        {
            { 0, "Ошибка, несуществующий textId" },
            { 1, "Взгляд на: "},
            { 2, "Пауза" },
            { 3, "Патроны" },
            { 4, "Обойма пуста. 'r' - перезарядить" },
            { 5, "'e' - взаимодействовать" },
            { 6, "Не удалось подобрать оружие" },
            { 7, "'e' - подобрать" },
            { 8, "Слишком далеко" }
        };

        // 
        //private static Dictionary<TextId, string> _dictionary = new Dictionary<TextId, string>()
        //{
        //    { TextId.None, "Ошибка, несуществующий textId" },
        //    { TextId.LookAt, "Взгляд на: "},
        //    { TextId.Pause, "Пауза" },
        //    { TextId.Ammunition, "Патроны" },
        //    { TextId.ClipEmpty, "Обойма пуста. 'r' - перезарядить" }
        //};

        #endregion


        #region Methods

        public static string GetText(int textId)
        {
            string str;
            if (_dictionary.ContainsKey(textId))
            {
                str = _dictionary[textId];
            }
            else
            {
                str = _dictionary[0];
            }
            return str;
        }

        #endregion
    }
}
