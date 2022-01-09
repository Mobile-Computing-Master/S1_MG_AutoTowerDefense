using Core.GameManager;
using Turrets;
using UnityEngine;
using Zenject;

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
            _localGameManager.StartUiElementDrag();
            Instantiate(gameObject, new Vector3(0, 0, 0), Quaternion.identity);

        }
        
        public void EndDragBuy()
        {
            _localGameManager.EndUiElementDrag();

        }
    }
}
