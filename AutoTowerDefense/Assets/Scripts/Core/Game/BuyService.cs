using Core.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Game
{
    public class BuyService : MonoBehaviour
    {
        private GameObject _buyPreviewElement;

        private bool _uiElementIsDragged = false;
        private GameObject _dragAttachedElement = null;

        public bool UiElementIsDragged => _uiElementIsDragged;

        public GameObject DraggedElement => _dragAttachedElement;

        public void StartUiElementDrag(GameObject go)
        {
            _dragAttachedElement = go;
            _uiElementIsDragged = true;
        }

        public void CancelUiElementDrag()
        {
            _dragAttachedElement = null;
            _uiElementIsDragged = false;
        }

        public GameObject GetDraggedElement()
        {
            return _dragAttachedElement;
        }
        
        public void SetElementForBuyPreview(GameObject go)
        {
            _buyPreviewElement = go;

            var buyableElement = _buyPreviewElement.GetComponent<IBuyable>();
            
            buyableElement.StartBuyPreview();
        }

        public GameObject BuyPreviewedElement()
        {
            var element = _buyPreviewElement.GetComponent<IBuyable>();
            element.Buy();
            var boughtElement = _buyPreviewElement;
            _buyPreviewElement = null;

            return boughtElement;
        }

        public void CancelBuyPreview()
        {
            if (!_buyPreviewElement) return;
            
            var buyableElement = _buyPreviewElement.GetComponent<IBuyable>();
            
            buyableElement.EndBuyPreview();
            
            Object.Destroy(_buyPreviewElement);
            _buyPreviewElement = null;
        }
    }
}
