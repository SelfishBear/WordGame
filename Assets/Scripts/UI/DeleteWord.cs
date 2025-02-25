using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DeleteWord : MonoBehaviour
    {
        [SerializeField] private Button _deleteButton;
        private string _word;
        private DictionaryUI _dictionaryUI;

        public void Initialize(string word, DictionaryUI dictionaryUI)
        {
            _word = word;
            _dictionaryUI = dictionaryUI;
        }

        private void OnEnable()
        {
            _deleteButton.onClick.AddListener(DeleteSingleWord);
        }

        private void DeleteSingleWord()
        {
            if (string.IsNullOrWhiteSpace(_word))
                return;

            if (PlayerPrefs.HasKey(_word))
            {
                PlayerPrefs.DeleteKey(_word);
                _dictionaryUI.RemoveWordFromDictionary(_word);
            }

            Destroy(gameObject);
        }
    }
}