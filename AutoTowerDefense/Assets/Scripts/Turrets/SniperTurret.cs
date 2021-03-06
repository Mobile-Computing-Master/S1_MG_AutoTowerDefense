using System;
using Core.Enums;
using Mobs;
using Path;
using UnityEngine;

namespace Turrets
{
    public class SniperTurret : TurretBase
    {
        public override TurretType Type { get; protected set; } = TurretType.Sniper;

        public float damage = 1000;

        public void Update()
        {
            if (!active) return;

            ReloadTime += Time.deltaTime;
            if (InRange.Count <= 0 || !(ReloadTime >= 1 / (hitsPerSecond * ReloadMultiplier))) return;
            
            Shoot(GetFurthestCreep());
            ReloadTime = 0;
        }

        protected override void Shoot(GameObject target)
        {
            var scaledDamage = tier switch
            {
                TurretTier.Tier1 => damage * DamageMultiplier,
                TurretTier.Tier2 => damage * DamageMultiplier * 1.25f,
                TurretTier.Tier3 => damage * DamageMultiplier * 1.5f,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            target.GetComponent<CreepBase>().hp -= scaledDamage;
        }
        
        private GameObject GetFurthestCreep ()
        {
            GameObject closestCreep = null;
            var closestDistanceSqr = Mathf.Infinity;
            var playerBasePoint = GameObject.Find("Path").GetComponent<PathMap>().playerBasePoint;

            foreach(var potentialTarget in InRange)
            {
                var directionToTarget = potentialTarget.transform.position - playerBasePoint;
                var dSqrToTarget = directionToTarget.sqrMagnitude;
                
                if (!(dSqrToTarget < closestDistanceSqr)) continue;
                closestDistanceSqr = dSqrToTarget;
                closestCreep = potentialTarget;
            }
     
            return closestCreep;
        }
    }
}