using System;
using Core.Interfaces;
using Turrets;
using Turrets.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Game
{
    public class BuyService : MonoBehaviour
    {
        private BankService _bankService;
        
        private GameObject _buyPreviewElement;

        private bool _uiElementIsDragged = false;
        private GameObject _dragAttachedElement = null;

        public bool UiElementIsDragged => _uiElementIsDragged;

        public GameObject DraggedElement => _dragAttachedElement;

        private void Start()
        {
            Initiate();
        }

        public void StartUiElementDrag(GameObject go)
        {
            if (_bankService.CanAfford(TurretPrices.GetPriceByTurretType(go.GetComponent<TurretBase>().Type)))
            {
                _dragAttachedElement = go;
                _uiElementIsDragged = true;   
            }
            else
            {
                Debug.Log("not enough money");
            }
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
            var element = _buyPreviewElement.GetComponent<TurretBase>();

            if (_bankService.TryWithdraw(TurretPrices.GetPriceByTurretType(element.Type)))
            {
                element.Buy();
                var boughtElement = _buyPreviewElement;
                _buyPreviewElement = null;

                return boughtElement;   
            }

            throw new Exception("Not enough money :-)");
        }

        public void CancelBuyPreview()
        {
            if (!_buyPreviewElement) return;
            
            var buyableElement = _buyPreviewElement.GetComponent<IBuyable>();
            
            buyableElement.EndBuyPreview();
            
            Object.Destroy(_buyPreviewElement);
            _buyPreviewElement = null;
        }

        private void Initiate()
        {
            _bankService = GameObject.Find("Context").GetComponent<BankService>();
        }
    }
}