using UnityEngine;

namespace Core.UI
{
    public class UiController : IUiController
    {
        private GameObject _mainSideDrawer = null;
        private Animation _mainSideDrawerAnimator = null;
        private bool _mainSideDrawerIsOpen = false;

        private const float MainSideDrawerSpeed = 1.8f;
        private const string DrawerAnimName = "drawer";

        public void OpenMainSideDrawer()
        {
            if (!_mainSideDrawer)
            {
                _mainSideDrawer = GameObject.Find("MainSideDrawer");
                Debug.Log(_mainSideDrawer.name);
                _mainSideDrawerAnimator = _mainSideDrawer.GetComponent<Animation>();
            }
            
            _mainSideDrawerAnimator[DrawerAnimName].speed = -MainSideDrawerSpeed;
            _mainSideDrawerAnimator[DrawerAnimName].time = _mainSideDrawerAnimator[DrawerAnimName].length;
            _mainSideDrawerAnimator.Play(DrawerAnimName);
            _mainSideDrawerIsOpen = true;
        }

        public void CloseMainSideDrawer()
        {
            _mainSideDrawerAnimator[DrawerAnimName].speed = MainSideDrawerSpeed;
            _mainSideDrawerAnimator.Play(DrawerAnimName);
            _mainSideDrawerIsOpen = false;

        }

        public bool ToggleMainSideDrawer()
        {
            if (_mainSideDrawerIsOpen)
            {
                CloseMainSideDrawer();
            }
            else
            {
                OpenMainSideDrawer();
            }

            return _mainSideDrawerIsOpen;
        }
    }
}
