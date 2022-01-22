using System;
using Core.GameManager;
using Core.Interfaces;
using Core.Map;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Turrets
{
    public abstract class TurretBase : MonoBehaviour, IBuyable, IPlaceable
    {
        public float range = 4f;
        public bool active = false;
        private LocalGameManager _localGameManager;
        private MapManager _mapManager;
        private Collider2D _bodyCollider = null;

        public delegate void SelectTurret(bool selected);
        public event SelectTurret OnTurretSelected;
        
        private bool _isSelected;

        private bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value != _isSelected)
                {
                    OnTurretSelected?.Invoke(value);
                    _isSelected = value;
                }
            }
        }

        public delegate void CanPlaceTurret(bool canPlace);
        public event CanPlaceTurret OnCanPlaceTurretChanged;
        private bool _isPlaceable = false;
        public bool IsPlaceable
        {
            get => _isPlaceable;
            set
            {
                if (value != _isPlaceable)
                {
                    OnCanPlaceTurretChanged?.Invoke(value);
                    _isPlaceable = value;
                }
            }
        }

        public abstract void BuyUpgrade();
        
        private void Awake()
        {
            Initiate();
            
            var collider = gameObject.GetComponent<CircleCollider2D>();

            if (!collider) throw new Exception("Attach a 2d circle collider!");

            collider.radius = range;
            
            Touch.onFingerDown += DetectClick;
            _bodyCollider = gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>();

            if (!_bodyCollider) throw new Exception("Attach a body with a 2D collider");
        }

        private void Initiate()
        {
            var sceneContext = GameObject.Find("Context");
            _localGameManager = sceneContext.GetComponent<LocalGameManager>();
            _mapManager = sceneContext.GetComponent<MapManager>();
        }

        private void DetectClick(Finger finger)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            
            // Check if body's collider is hit
            if (hit.collider != null && hit.collider == _bodyCollider)
            {
                if (!IsSelected)
                {
                    IsSelected = true;
                    _localGameManager.SetSelectedTurret(this);
                }
            }
            else
            {
                if (IsSelected)
                {
                    IsSelected = false;
                }   
            }
        }

        public int Buy()
        {
            active = true;
            
            // TODO: Return price?
            return 0;
        }

        public void StartBuyPreview()
        {
            IsSelected = true;
        }

        public void EndBuyPreview()
        {
            IsSelected = false;
        }

        public void UpdatePreview(Vector3 position)
        {
            IsPlaceable = !_mapManager.IsInProtectedSpace(position);
        }
    }
}
