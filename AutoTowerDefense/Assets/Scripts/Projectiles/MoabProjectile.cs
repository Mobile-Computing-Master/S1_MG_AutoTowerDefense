using Mobs;
using UnityEngine;

namespace Projectiles
{
    public class MoabProjectile : ProjectileBase
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out CreepBase creep)) return;
            
            creep.hp -= damage;
            Destroy(gameObject);
        }
    }
}