using System.Linq;
using Core.Enums;
using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class FreezeTurret : TurretBase
    {
        public override TurretType Type { get; protected set; } = TurretType.Freeze;

        public void Update()
        {
            if (!active) return;

            ReloadTime += Time.deltaTime;
            if (InRange.Count <= 0 || !(ReloadTime >= 1 / (hitsPerSecond * ReloadMultiplier))) return;
            
            Shoot(InRange.Last());
            ReloadTime = 0;
        }
    }
}