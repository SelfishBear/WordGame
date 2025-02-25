using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Button _dictionaryButton;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private AudioSource _audioSource;

        private const string MusicPrefKey = "MusicEnabled";

        private void OnEnable()
        {
            _dictionaryButton.onClick.AddListener(() => SwapScene());
        }

        private void SwapScene()
        {
            SceneManager.LoadScene("Dictionary");
        }
        private void Start()
        {
            bool isMusicOn = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1;
            _audioSource.mute = !isMusicOn;
            _musicToggle.isOn = isMusicOn;
            _musicToggle.onValueChanged.AddListener(ToggleMusic);
        }

        private void ToggleMusic(bool isOn)
        {
            _audioSource.mute = !isOn;
            PlayerPrefs.SetInt(MusicPrefKey, isOn ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}