using System;
using System.Collections.Generic;
using Core.Game;
using UnityEngine;

namespace Core.UI.Components
{
    public class HealthBar : MonoBehaviour
    {
        public GameObject fullHeartPrefab;
        public GameObject halfHeartPrefab;
        private List<GameObject> _hearts = new List<GameObject>();
        private GameObject _healthBar = null;
        private const float HeartOffset = 70.0f;
        
        private HealthService _healthService;
        
        private void OnEnable()
        {
            Initiate();
            
            _healthService.OnHealthChanged += UpdateHealthBar;
        }
        
        private void UpdateHealthBar(uint health)
        {
            _hearts.ForEach(Destroy);
            _hearts.Clear();
            
            if (health % 2 == 0)
            {
                if (health > 1)
                {
                    SpawnFullHearts(health);
                }
            }
            else
            {
                SpawnFullHearts(health - 1);
                SpawnHalfHeart();
            }
        }

        private void SpawnFullHearts(uint health)
        {
            for (int i = 0; i <= health; i = i + 2)
            {
                var h =Instantiate(fullHeartPrefab, _healthBar.transform);
                h.GetComponent<RectTransform>().localPosition = new Vector3((i * HeartOffset / 2) - _healthBar.GetComponent<RectTransform>().rect.width / 2, 0, 0);
                
                _hearts.Add(h);
            }
        }
        
        private void SpawnHalfHeart()
        {
            var h =Instantiate(halfHeartPrefab, _healthBar.transform);
            h.GetComponent<RectTransform>().localPosition = new Vector3((_hearts.Count * HeartOffset) - _healthBar.GetComponent<RectTransform>().rect.width / 2, 0, 0);
            _hearts.Add(h);
        }

        private void Initiate()
        {
            _healthService = GameObject.Find("Context").GetComponent<HealthService>();
            _healthBar = GameObject.Find("HealthBar");
        }
    }
}