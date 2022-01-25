using System;
using Core.Enums;
using Core.Game;
using Core.Interfaces;
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

        private void Start()
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
            // Clear existing buy previews
            _buyService.CancelBuyPreview();
            _uiController.HideTurretConfirmPopover();
            
            var go = _turretRoller.GetTurretPrefabBySlot(slot);
            
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
            _turretRepository.AddTurret(boughtItem);
            _buyService.CancelBuyPreview();
            _uiController.HideTurretConfirmPopover();
        }

        public void DeclineDragBuy()
        {
            _buyService.CancelBuyPreview();
            _uiController.HideTurretConfirmPopover();
        }
        
        private void Initiate()
        {
            var sceneContext = GameObject.Find("Context");
            _buyService = sceneContext.GetComponent<BuyService>();
            _uiController = sceneContext.GetComponent<UiController>();
            _turretRoller = sceneContext.GetComponent<TurretRoller>();
            _turretRepository = sceneContext.GetComponent<TurretRepository>();
        }
    }
}
