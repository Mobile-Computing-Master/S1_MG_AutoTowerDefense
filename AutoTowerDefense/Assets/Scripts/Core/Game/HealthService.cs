using Mobs;
using Path;
using UnityEngine;

namespace Core.Game
{
    public class HealthService : MonoBehaviour
    {
        public uint health = 20;
        public delegate void HealthChanged(uint health);
        public event HealthChanged OnHealthChanged;
        public delegate void PlayerDied();
        public event PlayerDied OnPlayerDied;

        private PathMap _path;
        private void Start()
        {
            _path = GameObject.Find("Path").GetComponent<PathMap>();
            OnHealthChanged?.Invoke(health);
        }

        private void Update()
        {
            var overlapSphere = Physics2D.OverlapCircleAll(_path.playerBasePoint, 0f);

            foreach (var col in overlapSphere)
            {
                if (!col.gameObject.TryGetComponent<CreepBase>(out var creep)) continue;
                
                Destroy(creep.gameObject);
                health = health > 0 ? health - 1 : 0;
                OnHealthChanged?.Invoke(health);
                if (health == 0) OnPlayerDied?.Invoke();
            }
        }
    }
}