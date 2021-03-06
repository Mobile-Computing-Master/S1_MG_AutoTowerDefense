using System;
using System.Collections.Generic;
using Core.Enums;
using Core.Game;
using Core.Interfaces;
using Core.Map;
using Mobs;
using Projectiles;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Turrets
{
    public abstract class TurretBase : MonoBehaviour, IBuyable, IPlaceable
    {
        public float range = 4f;
        public float hitsPerSecond = 2;
        public bool active = false;
        public GameObject projectilePrefab;
        public TurretTier tier = TurretTier.Tier1;
        public virtual TurretType Type { get; protected set; } = TurretType.None;
        public float DamageMultiplier { get; set; } = 1f;
        public float ReloadMultiplier { get; set; } = 1f;
        
        protected readonly List<GameObject> InRange = new List<GameObject>();
        protected float ReloadTime = 0f;
        
        private MapManager _mapManager;
        private TurretRepository _turretRepository;
        private Collider2D _bodyCollider = null;

        public delegate void SelectTurret(bool selected);
        public event SelectTurret OnTurretSelected;
        
        private bool _isSelected;

        private bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value == _isSelected) return;
                
                OnTurretSelected?.Invoke(value);
                _isSelected = value;
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
                if (value == _isPlaceable) return;
                
                OnCanPlaceTurretChanged?.Invoke(value);
                _isPlaceable = value;
            }
        }
        
        private void Awake()
        {
            Initiate();
            
            var circleCollider = gameObject.GetComponent<CircleCollider2D>();

            if (!circleCollider) throw new Exception("Attach a 2d circle collider!");

            circleCollider.radius = range;
            
            Touch.onFingerDown += DetectClick;
            _bodyCollider = gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>();

            if (!_bodyCollider) throw new Exception("Attach a body with a 2D collider");
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<CreepBase>() == null) return;
            if (InRange.Contains(other.gameObject)) return;
            
            InRange.Add(other.gameObject);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<CreepBase>() == null) return;
            if (!InRange.Contains(other.gameObject)) return;
            
            InRange.Remove(other.gameObject);
        }

        private void Initiate()
        {
            var sceneContext = GameObject.Find("Context");
            _mapManager = sceneContext.GetComponent<MapManager>();
            _turretRepository = sceneContext.GetComponent<TurretRepository>();
        }

        private void DetectClick(Finger finger)
        {
            if (Camera.main is null) return;

            var pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            var hit = Physics2D.Raycast(pos, Vector2.zero);
            
            // Check if body's collider is hit
            if (hit.collider != null && hit.collider == _bodyCollider)
            {
                if (IsSelected) return;
                
                IsSelected = true;
                _turretRepository.SetSelectedTurret(this);
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
        
        protected virtual void Shoot(GameObject target)
        {
            var projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity)
                .GetComponent<ProjectileBase>();

            projectile.damage = tier switch
            {
                TurretTier.Tier1 => projectile.damage * DamageMultiplier,
                TurretTier.Tier2 => projectile.damage * DamageMultiplier * 1.25f,
                TurretTier.Tier3 => projectile.damage * DamageMultiplier * 1.5f,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            projectile.target = target;
        }
    }
}
