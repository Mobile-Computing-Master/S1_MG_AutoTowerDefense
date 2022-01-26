using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI.Components
{
    public class HealthBar : MonoBehaviour
    {
        public GameObject fullHeartPrefab;
        public GameObject halfHeartPrefab;
        private List<GameObject> hearts = new List<GameObject>();
        
        private HealthService _healthService;
        
        private void OnEnable()
        {
            Initiate();
        }

        private void UpdateHealthBar()
        {
            int health = 20;

            if (health % 2 == 0)
            {
                SpawnHearts(health);
            }
            else
            {
                SpawnHearts(health - 1);
            }
        }

        private void SpawnHearts(int health)
        {
            
        }

        private void Initiate()
        {
            _healthService = GameObject.Find("Context").GetComponent<HealthService>();
        }
    }
}