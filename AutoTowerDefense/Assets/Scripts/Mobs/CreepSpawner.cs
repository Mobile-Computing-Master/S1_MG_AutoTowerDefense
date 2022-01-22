using System.Collections;
using System.Collections.Generic;
using Path;
using UnityEngine;

namespace Mobs
{
    public class CreepSpawner : MonoBehaviour
    {
        public GameObject creepPrefab;
        public PathMap pathMap;
        public float creepsPerSecond = 0.5f;
        private float _creepCooldown = 0.0f;
        
        void Update()
        {
            _creepCooldown += Time.deltaTime;

            if (_creepCooldown >= 1 / creepsPerSecond)
            {
                var t = Instantiate(creepPrefab, pathMap.creepSpawnPoint, Quaternion.identity);
                t.GetComponent<Creep>().Path = pathMap;
                _creepCooldown = 0;
            }
        }
    }
}

