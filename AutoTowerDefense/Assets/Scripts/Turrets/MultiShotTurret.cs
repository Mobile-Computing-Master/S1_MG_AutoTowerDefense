using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class MultiShotTurret : TurretBase
    {
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

        private void Shoot(GameObject target)
        {
            if (!active) return;

            var projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity)
                .GetComponent<ProjectileBase>();
            projectile.target = target;
        }
    }
}
