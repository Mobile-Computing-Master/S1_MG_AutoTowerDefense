using System;
using Core.Interfaces;
using Turrets;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Core.GameManager
{
    public class LocalGameManager : MonoBehaviour
    {
        private TurretBase _selectedTurret = null;
        private bool _uiElementIsDragged = false;
        private GameObject _dragAttachedElement = null;
        private GameObject _buyPreviewElement;

        public void SetSelectedTurret(TurretBase turret)
        {
            _selectedTurret = turret;
        }

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

        public void BuyPreviewedElement()
        {
            var element = _buyPreviewElement.GetComponent<IBuyable>();
            element.Buy();
            _buyPreviewElement = null;
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