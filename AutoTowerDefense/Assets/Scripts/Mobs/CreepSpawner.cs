using System.Collections.Generic;
using Path;
using UnityEngine;
using Random = System.Random;

namespace Mobs
{
    public class CreepSpawner : MonoBehaviour
    {
        public PathMap pathMap;
        public float creepsPerSecond = 1f;
        public int roundSize = 10;
        public List<GameObject> creepPrefabs = new List<GameObject>();
        private float HpMultiplier { get; set; } = 1f;
        private float _creepCooldown = 0f;
        private Queue<int> _spawnQueue = new Queue<int>();
        private bool _firstRound = true;

        private void Update()
        {
            if (_spawnQueue.Count == 0) return;
            
            _creepCooldown += Time.deltaTime;

            if (!(_creepCooldown >= 1 / creepsPerSecond)) return;
            
            var go = Instantiate(creepPrefabs[_spawnQueue.Dequeue()], pathMap.creepSpawnPoint, Quaternion.identity);
            var c = go.GetComponent<CreepBase>();
            c.Path = pathMap;
            c.hp *= HpMultiplier;
            _creepCooldown = 0;
        }

        public void FillCreepQueue()
        {
            var max = 0;
            if (_firstRound)
            {
                max = 0;
                _firstRound = false;
            }
            else
            {
                max = creepPrefabs.Count;
                creepsPerSecond = creepsPerSecond < 2.5f ? creepsPerSecond * 1.1f : creepsPerSecond;
                HpMultiplier = HpMultiplier < 2f ? HpMultiplier * 1.1f : HpMultiplier;
            }

            var random = new Random();
            var queue = new Queue<int>();
            for (var i = 0; i < roundSize; i++)
            {
                queue.Enqueue(random.Next(max));
            }

            _spawnQueue = queue;
        }

        public bool IsEverythingSpawned()
        {
            return _spawnQueue.Count == 0;
        }
    }
}

