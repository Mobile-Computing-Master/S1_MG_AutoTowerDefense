using System;
using Core.Enums;
using UnityEngine;

namespace Turrets
{
    public class DamageBuffTurret : TurretBase
    {
        public override TurretType Type { get; protected set; } = TurretType.DamageBuff;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!active) return;

            var turret = other.gameObject.GetComponentInParent<TurretBase>();
            if (turret == null) return;

            turret.DamageMultiplier = tier switch
            {
                TurretTier.Tier1 => 1.1f,
                TurretTier.Tier2 => 1.25f,
                TurretTier.Tier3 => 1.5f,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!active) return;
            
            var turret = other.gameObject.GetComponentInParent<TurretBase>();
            if (turret == null) return;

            turret.DamageMultiplier = 1f;
        }
    }
}