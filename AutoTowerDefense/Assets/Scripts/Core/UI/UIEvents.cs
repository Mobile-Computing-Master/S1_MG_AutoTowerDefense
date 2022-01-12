using System;
using Core.GameManager;
using Turrets;
using UnityEngine;
using Zenject;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Core.UI
{
    public class UIEvents : MonoBehaviour
    {
        [Inject]
        private IUiController _uiController;

        [Inject]
        private ILocalGameManager _localGameManager;

        public void ToggleSideDrawer()
        {
            _uiController.ToggleMainSideDrawer();
        }

        public void InitiateDragBuy(GameObject go)
        {
            // Clear existing buys
            DeclineDragBuy();
            
            var spawnPosition = Camera.main.ScreenToWorldPoint(Touch.activeFingers[0].screenPosition);
            spawnPosition.z = 0;
            var spawnedGameObject = Instantiate(go, spawnPosition, Quaternion.identity);
            
            _localGameManager.StartUiElementDrag(spawnedGameObject);
            _localGameManager.SetElementForBuyPreview(spawnedGameObject);
        }
        
        public void EndDragBuy()
        {
            _uiController.OpenTurretConfirmPopover(_localGameManager.DraggedElement.transform.position);
            _localGameManager.CancelUiElementDrag();
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
    }
}
