using System;
using System.Collections.Generic;
using Core.Enums;
using UnityEngine;

namespace Turrets
{
    public class DamageBuffTurret : TurretBase
    {
        public TurretType type = TurretType.DamageBuff;
        private List<GameObject> _buffed = new List<GameObject>();

        private void OnTriggerEnter2D(Collider2D other)
        {
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
            var turret = other.gameObject.GetComponentInParent<TurretBase>();
            if (turret == null) return;

            turret.DamageMultiplier = 1f;
        }
    }
}