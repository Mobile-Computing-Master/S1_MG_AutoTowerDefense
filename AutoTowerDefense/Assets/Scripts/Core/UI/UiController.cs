using System;
using System.Collections.Generic;
using Core.Game;
using Turrets;
using Turrets.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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

        private readonly List<GameObject> _turretFrames = new List<GameObject>();

        private TurretRoller _turretRoller;
        private HealthService _healthService;

        private int _activeSlot = -1;
        
        private GameObject _helpWrapper;
        private GameObject _helpButton;
        private GameObject _background;
        private GameObject _drawer;
        private GameObject _gameOver;
        public GameObject roundsCount;
        
        private void OnEnable()
        {
            Initiate();
            HideTrash();
            
            _turretRoller.OnRollChanged += UpdateTurretPrices;
            _healthService.OnPlayerDied += HealthServiceOnOnPlayerDied;
        }

        private void HealthServiceOnOnPlayerDied()
        {
            // TODO: Fix number of rounds
            ShowGameOver(1);
        }

        public void OpenMainSideDrawer()
        {
            _mainSideDrawerAnimator[DrawerAnimName].speed = MainSideDrawerSpeed;
            _mainSideDrawerAnimator.Play(DrawerAnimName);
            _mainSideDrawerIsOpen = true;
        }

        public void CloseMainSideDrawer()
        {
            _mainSideDrawerAnimator[DrawerAnimName].speed = -MainSideDrawerSpeed;
            _mainSideDrawerAnimator[DrawerAnimName].time = _mainSideDrawerAnimator[DrawerAnimName].length;
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

        public int GetActiveSlot()
        {
            return _activeSlot;
        }

        public void SetActiveSlot(int slot)
        {
            _activeSlot = slot;
        }
        
        public void ShowGameOver(int numberOfRounds)
        {
            roundsCount.GetComponent<Text>().text = numberOfRounds.ToString();
            _helpWrapper.SetActive(false);
            _helpButton.SetActive(false);
            _background.SetActive(true);
            _drawer.SetActive(false);
            _gameOver.SetActive(true);
        }
        
        private void UpdateTurretPrices(List<GameObject> turretPrefabs)
        {
            for (var i = 0; i < _turretRoller.numberOfSlots; i++)
            {
                _turretFrames[i].GetComponent<Text>().text =
                    $"{TurretPrices.GetPriceByTurretType(_turretRoller.GetTurretPrefabBySlot(i).GetComponent<TurretBase>().Type)}";
            }
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
            
            _turretRoller = GameObject.Find("Context").GetComponent<TurretRoller>();
            _healthService = GameObject.Find("Context").GetComponent<HealthService>();
            
            _turretFrames.Add(GameObject.Find("Frame_Coins_count_0"));
            _turretFrames.Add(GameObject.Find("Frame_Coins_count_1"));
            _turretFrames.Add(GameObject.Find("Frame_Coins_count_2"));

            _helpWrapper = GameObject.Find("HelpWrapper");
            _helpButton = GameObject.Find("HelpButton");
            _background = GameObject.Find("Background");
            _drawer = GameObject.Find("MainSideDrawer");
            _gameOver = GameObject.Find("GameOver");
        }
    }
}
