using System.Collections.Generic;
using Core.Enums;
using Turrets;
using UnityEngine;

namespace Core.Game
{
    public class TurretRepository
    {
        private List<GameObject> tier1 = new List<GameObject>();
        private List<GameObject> tier2 = new List<GameObject>();
        private List<GameObject> tier3 = new List<GameObject>();

        public void AddTurret(GameObject go)
        {
            var turretBase = go.GetComponent<TurretBase>();

            if (turretBase.tier != TurretTier.Tier1) return;
            
            // get number of same type in t1
            
        }
    }
}