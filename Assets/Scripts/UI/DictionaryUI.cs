using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class DictionaryUI : MonoBehaviour
    {
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private GameObject _addWordPanel;
        [SerializeField] private TMP_InputField _wordInputField;
        [SerializeField] private TMP_InputField _translationInputField;
        [SerializeField] private Button _hidePanelButton;
        [SerializeField] private GameObject _wordPrefab;
        [SerializeField] private GameObject _wordSpawnContainer;
        [SerializeField] private Button _bakcButton;

        private void Awake()
        {
            _addWordPanel.SetActive(false);
        }

        private void OnEnable()
        {
            DictionaryData.LoadWords();

            foreach (Transform child in _wordSpawnContainer.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var pair in DictionaryData.Words)
            {
                CreateWordUI(pair.Key, pair.Value);
            }

            _addButton.onClick.AddListener(ShowInputPanel);
            _hidePanelButton.onClick.AddListener(HidePanel);
            _confirmButton.onClick.AddListener(AddWord);
            _removeButton.onClick.AddListener(RemoveWord);
            _bakcButton.onClick.AddListener(SwitchToGameScene);
        }

        private void SwitchToGameScene()
        {
            DictionaryData.SaveWords();

            SceneManager.LoadScene("Game");
        }

        private void HidePanel()
        {
            _addWordPanel.SetActive(false);
        }

        private void ShowInputPanel()
        {
            _addWordPanel.SetActive(true);
        }

        private void AddWord()
        {
            string word = _wordInputField.text.Trim();
            string translation = _translationInputField.text.Trim();

            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(translation))
            {
                Debug.LogWarning("Слово или перевод не могут быть пустыми!");
                return;
            }

            if (DictionaryData.Words.ContainsKey(word))
            {
                Debug.LogWarning("Такое слово уже есть в словаре!");
                return;
            }

            DictionaryData.Words[word] = translation;
            DictionaryData.SaveWords();

            CreateWordUI(word, translation);
            
            _wordInputField.text = "";
            _translationInputField.text = "";
        }

        private void CreateWordUI(string word, string translation)
        {
            GameObject newWordObject = Instantiate(_wordPrefab, _wordSpawnContainer.transform);
            TMP_Text[] textComponents = newWordObject.GetComponentsInChildren<TMP_Text>();

            textComponents[0].text = word;
            textComponents[2].text = translation;

            DeleteWord deleteWordScript = newWordObject.GetComponent<DeleteWord>();
            if (!deleteWordScript) return;

            deleteWordScript.Initialize(word, this);
        }

        private void RemoveWord()
        {
            if (DictionaryData.Words.Count <= 0) return;

            string lastKey = "";

            foreach (string key in DictionaryData.Words.Keys)
            {
                lastKey = key;
            }

            DictionaryData.Words.Remove(lastKey);
            DictionaryData.SaveWords();
            RemoveWordFromUI();
        }

        private void RemoveWordFromUI()
        {
            if (_wordSpawnContainer.transform.childCount == 0) return;

            Transform lastWordUI = _wordSpawnContainer.transform.GetChild(_wordSpawnContainer.transform.childCount - 1);

            Destroy(lastWordUI.gameObject);
        }

        public void RemoveWordFromDictionary(string word)
        {
            if (DictionaryData.Words.ContainsKey(word))
            {
                DictionaryData.Words.Remove(word);
                DictionaryData.SaveWords();
            }
        }
    }
}