using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(() => StartGame());
            _quitButton.onClick.AddListener(() => QuitGame());
        }

        private void StartGame()
        {
            DictionaryData.LoadWords();

            SceneManager.LoadScene("Game");
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}