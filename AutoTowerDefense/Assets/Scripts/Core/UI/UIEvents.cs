using System;
using Core.GameManager;
using Core.Interfaces;
using Turrets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Zenject;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Core.UI
{
    public class UIEvents : MonoBehaviour
    {
        private UiController _uiController;
        private LocalGameManager _localGameManager;

        private void Start()
        {
            Initiate();
            TouchSimulation.Enable();
        }

        public void ToggleSideDrawer()
        {
            _uiController.ToggleMainSideDrawer();
        }

        public void InitiateDragBuy(GameObject go)
        {
            _uiController.CloseMainSideDrawer();
            var spawnPosition = Camera.main.ScreenToWorldPoint(Touch.activeFingers[0].screenPosition);
            spawnPosition.z = 0;
            var spawnedGameObject = Instantiate(go, spawnPosition, Quaternion.identity);

            _uiController.ShowTrash();
            _localGameManager.StartUiElementDrag(spawnedGameObject);
            _localGameManager.SetElementForBuyPreview(spawnedGameObject);
        }
        
        public void EndDragBuy()
        {
            var placeableElement = _localGameManager.DraggedElement.GetComponent<IPlaceable>();

            _uiController.OpenTurretConfirmPopover(_localGameManager.DraggedElement.transform.position);
            _localGameManager.CancelUiElementDrag();

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
            _localGameManager.BuyPreviewedElement();
            _localGameManager.CancelBuyPreview();
            _uiController.CloseTurretConfirmPopover();
        }

        public void DeclineDragBuy()
        {
            _localGameManager.CancelBuyPreview();
            _uiController.CloseTurretConfirmPopover();
        }
        
        private void Initiate()
        {
            var sceneContext = GameObject.Find("Context");
            _localGameManager = sceneContext.GetComponent<LocalGameManager>();
            _uiController = sceneContext.GetComponent<UiController>();
        }
    }
}
