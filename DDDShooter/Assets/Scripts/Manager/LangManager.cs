using System;
using System.Xml;
using UnityEngine;

using DddShooter;


namespace Geekbrains
{
    public class LangManager : Singleton<LangManager>
    {
        private XmlDocument _root;

        public event Action OnLanguageChange = delegate { };

        private void Awake()
        {
            Init("Language"); //, TextConstants.LANGUAGE_CODE_EN);
        }

        public string LanguageCode { get; private set; }

        public void SwitchLanguage(string languageCode)
        {
            if (LanguageCode != languageCode)
            {
                Init("Language", languageCode);
                OnLanguageChange();
            }
        }

        public void Init(string file, string languageCode = "")
        {
            _root = new XmlDocument();
            if (languageCode == "")
            {
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.Russian:
                        LanguageCode = TextConstants.LANGUAGE_CODE_RU;
                        break;
                    default:
                        LanguageCode = TextConstants.LANGUAGE_CODE_EN;
                        break;
                }
            }
            else
            {
                LanguageCode = languageCode;
            }
            var config = LoadResource(file);
            if (!config) return;
            _root.LoadXml(config.text);
        }

        public string Text(string level, string id)
        {
            if (_root == null)
            {
                return "[not init]";
                //return id;
            }
            string path = "Settings/group[@id='" + level + "']/string[@id='" + id +
            "']/text";
            XmlNode value = _root.SelectSingleNode(path);
            if (value == null)
            {
                return "[not found]";
                //return id;
            }
            return value.InnerText;
        }
        private string LocalizeResourceName(string resourceName)
        {
            return $"{resourceName}{LanguageCode}";
        }
        private TextAsset LoadResource(string resourceName)
        {
            return Resources.Load(LocalizeResourceName(resourceName),
            typeof(TextAsset)) as TextAsset;
        }
    }
}
