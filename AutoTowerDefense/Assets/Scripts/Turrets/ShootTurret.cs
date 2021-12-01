using System;
using System.Collections.Generic;
using System.Linq;
using Mobs;
using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class ShootTurret : TurretBase
    {
        public float hitsPerSecond = 2;
        public GameObject projectilePrefab;
        private readonly HashSet<GameObject> _inRange = new HashSet<GameObject>();
        private float _reloadTime = 0f;

        public void Update()
        {
            _reloadTime += Time.deltaTime;

            if (_inRange.Count > 0 && _reloadTime >= 1 / hitsPerSecond)
            {
                Shoot(_inRange.First());
                _reloadTime = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Creep>() != null)
            {
                _inRange.Add(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Creep>() != null)
            {
                _inRange.Remove(other.gameObject);
            }
        }
        
        private void Shoot(GameObject target)
        {
            var projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity)
                .GetComponent<ProjectileBase>();
            projectile.target = target;
        }
    }
}
