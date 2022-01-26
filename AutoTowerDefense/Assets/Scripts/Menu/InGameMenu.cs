using UnityEngine;

namespace Menu
{
    public class InGameMenu : MonoBehaviour
    {
        private GameObject _helpWrapper;
        private GameObject _helpButton;
        private GameObject _background;
        private GameObject _drawer;
        
        private void Start()
        {
            Initiate();
        }

        public void ShowHelp()
        {
            _helpWrapper.SetActive(true);
            _helpButton.SetActive(false);
            _background.SetActive(true);
            _drawer.SetActive(false);
            Time.timeScale = 0;
        }

        public void HideHelp()
        {
            _helpWrapper.SetActive(false);
            _helpButton.SetActive(true);
            _background.SetActive(false);
            _drawer.SetActive(true);
            Time.timeScale = 1;
        }

        private void Initiate()
        {
            _helpWrapper = GameObject.Find("HelpWrapper");
            _helpButton = GameObject.Find("HelpButton");
            _background = GameObject.Find("Background");
            _drawer = GameObject.Find("MainSideDrawer");
            
            HideHelp();
        }
    }
}
