using System.Collections.Generic;
using Core.Enums;
using Turrets;
using UnityEngine;

namespace Core.Game
{
    public class TurretRepository: MonoBehaviour
    {
        private List<GameObject> tier1 = new List<GameObject>();
        private List<GameObject> tier2 = new List<GameObject>();
        private List<GameObject> tier3 = new List<GameObject>();
        private const int MaxNumberOfSameTurret = 2;

        public void AddTurret(GameObject go)
        {
            var turretBase = go.GetComponent<TurretBase>();

            if (turretBase.tier != TurretTier.Tier1) return;

            var t1Siblings = tier1.FindAll(t => t.GetComponent<TurretBase>().type == turretBase.type);

            if (t1Siblings.Count == 2)
            {
                t1Siblings.ForEach(Destroy);
                turretBase.tier++;
                
                
                
                
                
                
                
                var t2Siblings = tier2.FindAll(t => t.GetComponent<TurretBase>().type == turretBase.type);

                if (t2Siblings.Count == 2)
                {
                    t2Siblings.ForEach(Destroy);
                    turretBase.tier++;
                    tier2.Remove()
                }
            }
        }
    }
}