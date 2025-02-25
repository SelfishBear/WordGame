using System;
using System.Linq;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameControl
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _wordText;
        [SerializeField] private TMP_InputField _translateInputField;
        [SerializeField] private TextMeshProUGUI _wrongWordText;
        [SerializeField]private Button _menuButton;
        private bool _useKey;
        private string _randomKeys;

        private void Awake()
        {
            _wrongWordText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _menuButton.onClick.AddListener(SwitchToMenu);
            _translateInputField.onSubmit.AddListener(OnInputSubmit);
        }

        private void OnInputSubmit(string userInput)
        {
            CheckWord();
            _translateInputField.ActivateInputField();
        }
        private void SwitchToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        private void Start()
        {
            SpawnWord();
        }

        
        private void CheckWord()
        {
            if (DictionaryData.Words.ContainsValue(_translateInputField.text.Trim()))
            {
                Debug.Log("Word found");
                _wrongWordText.gameObject.SetActive(false);
                SpawnWord();
            }
            else if (DictionaryData.Words.ContainsKey(_translateInputField.text.Trim()))
            {
                Debug.Log("Word found");
                _wrongWordText.gameObject.SetActive(false);
                SpawnWord();
            }
            else
            {
                if (_useKey)
                {
                    _wrongWordText.text = $"НЕПРАВИЛЬНО, ПЕРЕВОД : {DictionaryData.Words[_randomKeys]}";
                    _wrongWordText.gameObject.SetActive(true);
                    Debug.Log("Word not found");
                }
                else
                {
                    _wrongWordText.text = $"НЕПРАВИЛЬНО, ПЕРЕВОД : {_randomKeys}";
                    _wrongWordText.gameObject.SetActive(true);
                    Debug.Log("Word not found");
                }
            }
            _translateInputField.text = "";
        }

        private void SpawnWord()
        {
            var keys = DictionaryData.Words.Keys.ToList();
            if (keys.Count == 0)
            {
                Debug.LogWarning("Словарь пуст!");
                return;
            }

            _randomKeys = keys[Random.Range(0, keys.Count)];
            
            _useKey = Random.Range(0, 2) == 0;
            if (_useKey)
            {
                _wordText.text = _randomKeys;
            }
            else
            {
                _wordText.text = DictionaryData.Words[_randomKeys];
            }
        }
    }
}