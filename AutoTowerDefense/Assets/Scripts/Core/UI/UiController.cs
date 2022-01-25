using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Core.UI
{
    public class UiController : MonoBehaviour
    {
        public Vector3 LastFingerTouchScreenPosition { get; private set; }
        
        private GameObject _mainSideDrawer = null;
        private Animation _mainSideDrawerAnimator = null;
        private bool _mainSideDrawerIsOpen = false;
        
        private const float MainSideDrawerSpeed = 1.8f;
        private const string DrawerAnimName = "drawer";

        private RectTransform _turretConfirmPopoverRect = null;
        private GameObject _turretConfirmPopoverGameObject = null;

        private GameObject _trash = null;
        private RectTransform _trashRect = null;

        private void Start()
        {
            Initiate();
            HideTrash();
        }

        public void OpenMainSideDrawer()
        {
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

        public void ShowTurretConfirmPopover(Vector3 position)
        {
            if (Camera.main is null) return;
            
            var transformedVector = Camera.main.WorldToScreenPoint(position);
            transformedVector.z = _turretConfirmPopoverRect.position.z;
            
            _turretConfirmPopoverRect.position = transformedVector;
            
            _turretConfirmPopoverGameObject.SetActive(true);
        }
        public void HideTurretConfirmPopover()
        {
            if (_turretConfirmPopoverGameObject is null) return;
            
            _turretConfirmPopoverGameObject.SetActive(false);
        }

        public void ShowTrash()
        {
            _trash.SetActive(true);
        }
        
        public void HideTrash()
        {
            _trash.GetComponent<CanvasRenderer>().gameObject.SetActive(false);
        }
        
        private void Initiate()
        {
            Touch.onFingerUp += finger => LastFingerTouchScreenPosition = finger.screenPosition;
            
            _mainSideDrawer = GameObject.Find("MainSideDrawer");
            _mainSideDrawerAnimator = _mainSideDrawer.GetComponent<Animation>();
            
            _turretConfirmPopoverGameObject = GameObject.Find("TurretConfirmPopover");
            _turretConfirmPopoverRect = _turretConfirmPopoverGameObject.GetComponent<Canvas>().GetComponent<RectTransform>();
            
            _trash = GameObject.Find("Trash");
            _trashRect = _trash.GetComponent<RectTransform>();

        }
    }
}
