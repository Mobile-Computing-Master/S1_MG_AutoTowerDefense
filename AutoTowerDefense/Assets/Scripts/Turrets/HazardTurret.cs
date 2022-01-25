using System;
using Core.Enums;
using Mobs;
using UnityEngine;

namespace Turrets
{
    public class HazardTurret : TurretBase
    {
        public override TurretType Type { get; protected set; } = TurretType.Hazard;

        public float damage = 5;

        public void Update()
        {
            if (!active) return;

            ReloadTime += Time.deltaTime;
            if (InRange.Count <= 0 || !(ReloadTime >= 1 / hitsPerSecond)) return;
            
            foreach (var enemy in InRange)
            {
                Shoot(enemy);
            }
            ReloadTime = 0;
        }

        protected override void Shoot(GameObject target)
        {
            damage = tier switch
            {
                TurretTier.Tier1 => damage * DamageMultiplier,
                TurretTier.Tier2 => damage * DamageMultiplier * 1.25f,
                TurretTier.Tier3 => damage * DamageMultiplier * 1.5f,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            target.GetComponent<Creep>().hp -= damage;
        }
    }
}