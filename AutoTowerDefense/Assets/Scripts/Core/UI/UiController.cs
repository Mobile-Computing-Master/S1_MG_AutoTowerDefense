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

        private RectTransform _turretConfirmPopoverRect = null;
        private GameObject _turretConfirmPopoverGameObject = null;
        
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

        public void OpenTurretConfirmPopover(Vector3 position)
        {
            if (!_turretConfirmPopoverRect)
            {
                _turretConfirmPopoverGameObject = GameObject.Find("TurretConfirmPopover");
                _turretConfirmPopoverRect = _turretConfirmPopoverGameObject.GetComponent<Canvas>().GetComponent<RectTransform>();
            }

            var transformedVector = Camera.main.WorldToScreenPoint(position);
            transformedVector.z = _turretConfirmPopoverRect.position.z;
            
            _turretConfirmPopoverRect.position = transformedVector;
            
            _turretConfirmPopoverGameObject.SetActive(true);
        }

        public void CloseTurretConfirmPopover()
        {
            if (!_turretConfirmPopoverRect)
            {
                _turretConfirmPopoverGameObject = GameObject.Find("TurretConfirmPopover");
                _turretConfirmPopoverRect = _turretConfirmPopoverGameObject.GetComponent<Canvas>().GetComponent<RectTransform>();
            }
            
            _turretConfirmPopoverGameObject.SetActive(false);
        }
    }
}
