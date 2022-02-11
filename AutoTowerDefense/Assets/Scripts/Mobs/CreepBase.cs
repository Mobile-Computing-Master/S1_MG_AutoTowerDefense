using System;
using Core.Game;
using Path;
using UnityEngine;

namespace Mobs
{
    public abstract class CreepBase : MonoBehaviour
    {
        public PathMap Path { get; set; }
        public float speed = 1;
        public float hp = 100;
        private int _currentPointIndex = 0;
        private BankService _bankService;

        private void Start()
        {
            Initiate();
        }

        private void Update()
        {
            var nextPoint = _currentPointIndex < Path.points.Count ? Path.points[_currentPointIndex] : Path.playerBasePoint;
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

            if (hp > 0) return;
            _bankService.Add(1);
            Destroy(gameObject);
        }
        
        private void Initiate()
        {
            _bankService = GameObject.Find("Context").GetComponent<BankService>();
        }
    }
}

