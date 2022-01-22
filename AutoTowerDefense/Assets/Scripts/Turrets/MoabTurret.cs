using System.Collections.Generic;
using System.Linq;
using Mobs;
using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class MoabTurret : TurretBase
    {
        public float hitsPerSecond = 1000;
        public GameObject projectilePrefab;
        private readonly HashSet<GameObject> _inRange = new HashSet<GameObject>();
        private float _reloadTime = 0f;

        public override void BuyUpgrade()
        {
            throw new System.NotImplementedException();
        }
        
        public void Update()
        {
            if (!active) return;

            _reloadTime += Time.deltaTime;

            if (_inRange.Count > 0 && _reloadTime >= 1 / hitsPerSecond)
            {
                Shoot(GetClosestCreep());
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
                .GetComponent<FreezeProjectile>();
            projectile.target = target;
        }
        
        private GameObject GetClosestCreep ()
        {
            GameObject closestCreep = null;
            var closestDistanceSqr = Mathf.Infinity;
            var currentPosition = transform.position;
            
            foreach(var potentialTarget in _inRange)
            {
                var directionToTarget = potentialTarget.transform.position - currentPosition;
                var dSqrToTarget = directionToTarget.sqrMagnitude;
                
                if (!(dSqrToTarget < closestDistanceSqr)) continue;
                closestDistanceSqr = dSqrToTarget;
                closestCreep = potentialTarget;
            }
     
            return closestCreep;
        }
    }
}