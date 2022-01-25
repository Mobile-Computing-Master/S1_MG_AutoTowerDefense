using Turrets;
using UnityEngine;

namespace Core.GameManager
{
    public interface ILocalGameManager
    {
        public bool UiElementIsDragged { get; }
        public GameObject DraggedElement { get; }
        
        public void SetSelectedTurret(TurretBase turret);

        public void StartUiElementDrag(GameObject gameObject);
        
        public void CancelUiElementDrag();

        void SetElementForBuyPreview(GameObject gameObject);
        void BuyPreviewedElement();
        void CancelBuyPreview();
    }
}