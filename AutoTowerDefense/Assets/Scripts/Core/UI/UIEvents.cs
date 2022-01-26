using System;
using Core.Enums;
using Core.Game;
using Core.Interfaces;
using Core.UI.Components;
using Turrets;
using Turrets.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Core.UI
{
    public class UIEvents : MonoBehaviour
    {
        private UiController _uiController;
        private BuyService _buyService;
        private TurretRoller _turretRoller;
        private TurretRepository _turretRepository;
        private BankService _bankService;
        private const string TurretFrameName = "turretFrame_";

        private void OnEnable()
        {
            Initiate();
            TouchSimulation.Enable();
        }

        public void ToggleSideDrawer()
        {
            _uiController.ToggleMainSideDrawer();
        }

        public void InitiateTurretDragBuy(int slot)
        {
            _uiController.SetActiveSlot(slot);
            
            // Clear existing buy previews
            _buyService.CancelBuyPreview();
            _uiController.HideTurretConfirmPopover();
            
            var go = _turretRoller.GetTurretPrefabBySlot(slot);

            if (SlotAlreadyBought(slot)) return;
            
            if (!_bankService.CanAfford(TurretPrices.GetPriceByTurretType(go.GetComponent<TurretBase>().Type))) return;

            _uiController.CloseMainSideDrawer();
            
            if (Camera.main is null) return;

            var spawnPosition = Camera.main.ScreenToWorldPoint(Touch.activeFingers[0].screenPosition);
            spawnPosition.z = 0;
            var spawnedGameObject = Instantiate(go, spawnPosition, Quaternion.identity);

            _uiController.ShowTrash();
            _buyService.StartUiElementDrag(spawnedGameObject);
            _buyService.SetElementForBuyPreview(spawnedGameObject);
        }

        public void EndTurretDragBuy()
        {
            if (_buyService.DraggedElement is null) return;
            
            var placeableElement = _buyService.DraggedElement.GetComponent<IPlaceable>();

            _uiController.ShowTurretConfirmPopover(_buyService.DraggedElement.transform.position);
            _buyService.CancelUiElementDrag();

            if (EventSystem.current.IsPointerOverGameObject() || !placeableElement.IsPlaceable)
            {
                // Element is not placeable at this position, e. g. Turret is placed on top of line
                // Or Element is dragged over trash
                DeclineDragBuy();
            }

            _uiController.HideTrash();
            _uiController.OpenMainSideDrawer();

        }

        public void ConfirmDragBuy()
        {
            var boughtItem = _buyService.BuyPreviewedElement();
            DisableSlot(_uiController.GetActiveSlot());
            
            _turretRoller.DestroyPreviewInSlot(_uiController.GetActiveSlot());
            _uiController.SetActiveSlot(-1);
            _turretRepository.AddTurret(boughtItem);
            _buyService.CancelBuyPreview();
            _uiController.HideTurretConfirmPopover();
        }

        public void DeclineDragBuy()
        {
            _uiController.SetActiveSlot(-1);

            _buyService.CancelBuyPreview();
            _uiController.HideTurretConfirmPopover();
        }

        public void Reroll()
        {
            if (!_bankService.CanAfford(_turretRoller.rerollCosts)) return;

            if (_bankService.TryWithdraw(_turretRoller.rerollCosts))
            {
                _turretRoller.ResetTurretSlots();
                _turretRoller.RollTurrets();
            }
        }

        private bool SlotAlreadyBought(int slot)
        {
            var locker = GameObject.Find($"{TurretFrameName}{slot}").GetComponent<TurretFrameLocker>();
            return locker.GetAlreadyBought();
        }

        private void DisableSlot(int slot)
        {
            var locker = GameObject.Find($"{TurretFrameName}{slot}").GetComponent<TurretFrameLocker>();
            locker.SetAlreadyBought(true);
        }
        
        private void Initiate()
        {
            var sceneContext = GameObject.Find("Context");
            _buyService = sceneContext.GetComponent<BuyService>();
            _uiController = sceneContext.GetComponent<UiController>();
            _turretRoller = sceneContext.GetComponent<TurretRoller>();
            _turretRepository = sceneContext.GetComponent<TurretRepository>();
            _bankService = sceneContext.GetComponent<BankService>();
        }
    }
}
