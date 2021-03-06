using Core.Enums;
using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class MoabTurret : TurretBase
    {
        public override TurretType Type { get; protected set; } = TurretType.Moab;

        public void Update()
        {
            if (!active) return;

            ReloadTime += Time.deltaTime;
            if (InRange.Count <= 0 || !(ReloadTime >= 1 / (hitsPerSecond * ReloadMultiplier))) return;
            
            Shoot(GetClosestCreep());
            ReloadTime = 0;
        }

        private GameObject GetClosestCreep ()
        {
            GameObject closestCreep = null;
            var closestDistanceSqr = Mathf.Infinity;
            var currentPosition = transform.position;
            
            foreach(var potentialTarget in InRange)
            {
                var directionToTarget = potentialTarget.transform.position - currentPosition;
                var dSqrToTarget = directionToTarget.sqrMagnitude;
                
                if (!(dSqrToTarget < closestDistanceSqr)) continue;
                closestDistanceSqr = dSqrToTarget;
                closestCreep = potentialTarget;
            }
     
            return closestCreep;
        }
    }
}