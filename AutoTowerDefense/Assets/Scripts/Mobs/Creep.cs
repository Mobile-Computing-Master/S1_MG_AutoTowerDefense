using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Path;
using UnityEngine;

namespace Mobs
{
    public class Creep : MonoBehaviour
    {
        public PathMap Path { get; set; }
        public float speed = 1;
        public float hp = 100;
        private int _currentPointIndex = 0;

        private void Update()
        {
            var nextPoint = _currentPointIndex < Path.points.Count ? Path.points[_currentPointIndex] : Path.player2Base;
            var currentPosition = transform.position;

            if (transform.position != nextPoint)
            {
                var newPosition = Vector3.MoveTowards(currentPosition, nextPoint, speed * Time.deltaTime);
                transform.position = newPosition;
            }
            else
            {
                _currentPointIndex++;
            }

            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

