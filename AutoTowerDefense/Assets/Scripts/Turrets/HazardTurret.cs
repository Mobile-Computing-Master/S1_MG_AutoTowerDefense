using System.Collections.Generic;
using Mobs;
using UnityEngine;

namespace Turrets
{
    public class HazardTurret : TurretBase
    {
        public float hitsPerSecond = 1000;
        public float damage = 5;
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
                foreach (var enemy in _inRange)
                {
                    Shoot(enemy);
                }
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
            target.GetComponent<Creep>().hp -= damage;
        }
    }
}