using System;
using System.Collections;
using System.Collections.Generic;
using Core.Eums;
using Mobs;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileBase : MonoBehaviour
    {
        public float speed = 100f;
        public float damage = 50f;
        public GameObject target;
        private Vector3 _normalizedDirection;

        private void Start()
        {
            _normalizedDirection = (target.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(_normalizedDirection * speed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetInstanceID() == target.GetInstanceID())
            {
                if (target == null) return;
                target.GetComponent<Creep>().hp -= damage;
                Destroy(gameObject);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
