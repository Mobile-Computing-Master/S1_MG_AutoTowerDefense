using System.Linq;
using Core.Enums;
using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class FreezeTurret : TurretBase
    {
        public new TurretType type = TurretType.Freeze;
        
        public override void BuyUpgrade()
        {
            throw new System.NotImplementedException();
        }
        
        public void Update()
        {
            if (!active) return;

            ReloadTime += Time.deltaTime;
            if (InRange.Count <= 0 || !(ReloadTime >= 1 / hitsPerSecond)) return;
            
            Shoot(InRange.Last());
            ReloadTime = 0;
        }
    }
}