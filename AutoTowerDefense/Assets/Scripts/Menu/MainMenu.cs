using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        private GameObject _helpWrapper;
        private GameObject _mainWrapper;

        private void Start()
        {
            Initiate();
        }

        public void StartMap(int mapNumber)
        {
            // TODO: Add other maps
            SceneManager.LoadScene("Scenes/main");
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void ShowHints()
        {
            _helpWrapper.SetActive(true);
            _mainWrapper.SetActive(false);
        }

        public void HideHints()
        {
            _helpWrapper.SetActive(false);
            _mainWrapper.SetActive(true);
        }
    
        private void Initiate()
        {
            _helpWrapper = GameObject.Find("HelpWrapper");
            _mainWrapper = GameObject.Find("MainWrapper");
        
            _helpWrapper.SetActive(false);
        }
    }
}
