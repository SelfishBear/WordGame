using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public static class DictionaryData
    {
        public static Dictionary<string, string> Words { get; set; } = new Dictionary<string, string>();
        
        public static void LoadWords()
        {
            Words.Clear(); // Очищаем словарь перед загрузкой

            string savedKeys = PlayerPrefs.GetString("WordKeys", "");

            foreach (string key in savedKeys.Split('|'))
            {
                if (!string.IsNullOrWhiteSpace(key) && !Words.ContainsKey(key))
                {
                    string translation = PlayerPrefs.GetString(key, "");
                    Words[key] = translation;
                }
            }
        }

        public static void SaveWords()
        {
            PlayerPrefs.SetString("WordKeys", string.Join("|", Words.Keys));
            foreach (var pair in Words)
            {
                PlayerPrefs.SetString(pair.Key, pair.Value);
            }
            PlayerPrefs.Save();
        }
    }
}