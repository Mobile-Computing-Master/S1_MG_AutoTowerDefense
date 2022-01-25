using System.Linq;
using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class BasicTurret : TurretBase 
    {
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
        
        private void Shoot(GameObject target)
        {
            var projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity)
                .GetComponent<BasicProjectile>();
            projectile.target = target;
        }
    }
}