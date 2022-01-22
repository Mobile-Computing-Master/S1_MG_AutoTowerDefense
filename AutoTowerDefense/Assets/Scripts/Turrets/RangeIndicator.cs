using System;
using UnityEngine;

namespace Turrets
{
    public class RangeIndicator : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        
        private void Start()
        {
            Initialize();
            
            var turretBase = gameObject.GetComponentInParent<TurretBase>();

            if (!turretBase) throw new Exception("Attach a turret base!");
        
            // Set indicator size according to range
            gameObject.transform.localScale = new Vector3(turretBase.range * 2, turretBase.range * 2, 0);

            turretBase.OnTurretSelected += ChangeVisibilityAndPreserveColor;
            turretBase.OnCanPlaceTurretChanged += ChangeColor;
        }

        private void Initialize()
        {
            _sprite = gameObject.GetComponent<SpriteRenderer>();
        }

        private void ChangeVisibilityAndPreserveColor(bool selected)
        {
            _sprite.color = Color.white;
            gameObject.SetActive(selected);
        }
        
        private void ChangeColor(bool canPlace)
        {
            if (canPlace)
            {
                _sprite.color = Color.green;
            }
            else
            {
                _sprite.color = Color.red;

            }
        }
    }
}
