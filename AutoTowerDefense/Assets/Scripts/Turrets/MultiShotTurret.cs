using Core.Enums;
using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class MultiShotTurret : TurretBase
    {
        public override TurretType Type { get; protected set; } = TurretType.Multi;

        private void Update()
        {
            if (!active) return;

            ReloadTime += Time.deltaTime;
            if (InRange.Count <= 0 || !(ReloadTime >= 1 / (hitsPerSecond * ReloadMultiplier))) return;
            
            foreach (var enemy in InRange)
            {
                Shoot(enemy);
            }
            ReloadTime = 0;
        }
    }
}
