using System;
using Mobs;
using UnityEngine;

namespace Core.Game
{
    public class RoundService : MonoBehaviour
    {
        public float timeBetweenRounds = 5f;
        public float creepIncreaseMultiplier = 1.5f;
        
        private CreepSpawner _spawner;
        private float _roundCooldown = 0f;
        private HealthService _healthService;
        private bool _playerDied = false;

        private void Start()
        {
            _spawner = GameObject.Find("Spawner").GetComponent<CreepSpawner>();
            _healthService = GameObject.Find("Context").GetComponent<HealthService>();
            _spawner.FillCreepQueue();
            _healthService.OnPlayerDied += OnPlayerDied;
        }

        private void Update()
        {
            if (!_spawner.IsEverythingSpawned() || _playerDied) return;
            var creeps = FindObjectsOfType<CreepBase>();
            if (creeps.Length > 0) return;

            _roundCooldown += Time.deltaTime;
            if (_roundCooldown < timeBetweenRounds) return;

            _spawner.roundSize = (int)(_spawner.roundSize * creepIncreaseMultiplier);
            _spawner.FillCreepQueue();
            _roundCooldown = 0;
        }

        private void OnPlayerDied()
        {
            _playerDied = true;
        }
    }
}