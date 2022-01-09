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

        public void InitiateDragBuy(GameObject gameObject)
        {
            var spawnPosition = Camera.main.ScreenToWorldPoint(Touch.activeFingers[0].screenPosition);
            spawnPosition.z = 0;
            var spawnedGameObject = Instantiate(gameObject, spawnPosition, Quaternion.identity);
            
            _localGameManager.StartUiElementDrag(spawnedGameObject);
        }
        
        public void EndDragBuy()
        {
            _localGameManager.CancelUiElementDrag();

        }
    }
}
