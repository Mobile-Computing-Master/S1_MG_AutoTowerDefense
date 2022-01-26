using System;
using Mobs;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Projectiles
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        public float speed = 100f;
        public float damage = 50f;
        public GameObject target;
        private Vector3 _normalizedDirection;
        private float _timeALive = 0f;

        private void Start()
        {
            if (target == null) return;
            _normalizedDirection = (target.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(_normalizedDirection * speed);
        }

        private void Update()
        {
            _timeALive += Time.deltaTime;
            if (_timeALive < 15) return;
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetInstanceID() != target.GetInstanceID()) return;
            if (target == null) return;
            
            target.GetComponent<CreepBase>().hp -= damage;
            Destroy(gameObject);
        }
    }
}