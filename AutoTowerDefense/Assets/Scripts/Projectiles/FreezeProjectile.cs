using Mobs;
using UnityEngine;

namespace Projectiles
{
    public class FreezeProjectile : ProjectileBase
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetInstanceID() != target.GetInstanceID()) return;
            if (target == null) return;
            
            var creep = target.GetComponent<CreepBase>();
            creep.hp -= damage;
            creep.speed = creep.speed > 0.5 ? creep.speed *= 0.995f : creep.speed;
            Destroy(gameObject);
        }
    }
}