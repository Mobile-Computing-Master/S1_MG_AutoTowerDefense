using Core.Enums;
using Mobs;
using Path;
using UnityEngine;

namespace Turrets
{
    public class SniperTurret : TurretBase
    {
        public new TurretType type = TurretType.Sniper;
        public float damage = 1000;

        public override void BuyUpgrade()
        {
            throw new System.NotImplementedException();
        }
        
        public void Update()
        {
            if (!active) return;

            ReloadTime += Time.deltaTime;
            if (InRange.Count <= 0 || !(ReloadTime >= 1 / hitsPerSecond)) return;
            
            Shoot(GetFurthestCreep());
            ReloadTime = 0;
        }

        protected override void Shoot(GameObject target)
        {
            target.GetComponent<Creep>().hp -= damage;
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