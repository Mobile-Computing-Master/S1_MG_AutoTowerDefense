using System.Linq;
using Core.Enums;
using UnityEngine;

namespace Turrets
{
    public class BasicTurret : TurretBase
    {
        public override TurretType Type { get; protected set; } = TurretType.Basic;
        
        public void Update()
        {
            if (!active) return;

            ReloadTime += Time.deltaTime;
            if (InRange.Count <= 0 || !(ReloadTime >= 1 / (hitsPerSecond * ReloadMultiplier))) return;
            
            Shoot(InRange.First());
            ReloadTime = 0;
        }
    }
}
