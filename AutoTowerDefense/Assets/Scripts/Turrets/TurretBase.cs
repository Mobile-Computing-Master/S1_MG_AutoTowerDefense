using System;
using Core.GameManager;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Zenject;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Turrets
{
    public abstract class TurretBase : MonoBehaviour, IBuyable
    {
        public float range = 4f;
        public bool active = false;

        public delegate void SelectTurret(bool selected);
        public event SelectTurret OnTurretSelected;

        [Inject]
        private ILocalGameManager _localGameManager;
        private bool _isSelected;
        private Collider2D _bodyCollider = null;

        private bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (value != _isSelected)
                {
                    OnTurretSelected?.Invoke(value);
                    _isSelected = value;
                }
            }
        }

        public abstract void BuyUpgrade();
        
        private void Start()
        {
            var collider = gameObject.GetComponent<CircleCollider2D>();

            if (!collider) throw new Exception("Attach a 2d circle collider!");

            collider.radius = range;
            
            Touch.onFingerDown += DetectClick;
            _bodyCollider = gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>();

            if (!_bodyCollider) throw new Exception("Attach a body with a 2D collider");
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
    }
}
