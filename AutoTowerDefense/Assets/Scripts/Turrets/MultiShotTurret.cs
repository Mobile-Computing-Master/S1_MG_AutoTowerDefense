using Core.Enums;
using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class MultiShotTurret : TurretBase
    {
        public new TurretType type = TurretType.Multi;
        
        public override void BuyUpgrade()
        {
            throw new System.NotImplementedException();
        }
        
        private void Update()
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
    }
}
