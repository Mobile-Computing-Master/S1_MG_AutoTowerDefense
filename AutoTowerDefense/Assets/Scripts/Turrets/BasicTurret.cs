using System.Linq;
using Core.Enums;
using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class BasicTurret : TurretBase
    {
        public new TurretType type = TurretType.Basic;
        
        public override void BuyUpgrade()
        {
            throw new System.NotImplementedException();
        }
        
        public void Update()
        {
            if (!active) return;

            ReloadTime += Time.deltaTime;
            if (InRange.Count <= 0 || !(ReloadTime >= 1 / hitsPerSecond)) return;
            
            Shoot(InRange.First());
            ReloadTime = 0;
        }
    }
}
