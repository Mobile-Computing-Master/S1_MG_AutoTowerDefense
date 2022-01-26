using UnityEngine;

namespace Menu
{
    public class InGameMenu : MonoBehaviour
    {
        private GameObject _helpWrapper;
        private GameObject _helpButton;
        private GameObject _background;

        private void Start()
        {
            Initiate();
        }

        public void ShowHelp()
        {
            _helpWrapper.SetActive(true);
            _helpButton.SetActive(false);
            _background.SetActive(true);
            Time.timeScale = 0;
        }

        public void HideHelp()
        {
            _helpWrapper.SetActive(false);
            _helpButton.SetActive(true);
            _background.SetActive(false);
            Time.timeScale = 1;
        }

        private void Initiate()
        {
            _helpWrapper = GameObject.Find("HelpWrapper");
            _helpButton = GameObject.Find("HelpButton");
            _background = GameObject.Find("Background");
            
            HideHelp();
        }
    }
}
