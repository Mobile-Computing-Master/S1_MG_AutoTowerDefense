using System;
using System.Collections.Generic;
using Path;
using UnityEngine;
using Random = System.Random;

namespace Mobs
{
    public class CreepSpawner : MonoBehaviour
    {
        public PathMap pathMap;
        public float creepsPerSecond = 0.5f;
        public int roundSize = 10;
        public List<GameObject> creepPrefabs = new List<GameObject>();
        private float _creepCooldown = 0.25f;
        private readonly Stack<int> _spawnQueue = new Stack<int>(); 

        private void Start()
        {
            var max = creepPrefabs.Count;
            var random = new Random();
            for (var i = 0; i < roundSize; i++)
            {
                _spawnQueue.Push(random.Next(max));
            }
        }

        private void Update()
        {
            if (_spawnQueue.Count == 0) return;
            
            _creepCooldown += Time.deltaTime;

            if (!(_creepCooldown >= 1 / creepsPerSecond)) return;
            
            var t = Instantiate(creepPrefabs[_spawnQueue.Pop()], pathMap.creepSpawnPoint, Quaternion.identity);
            t.GetComponent<CreepBase>().Path = pathMap;
            _creepCooldown = 0;
        }
    }
}

