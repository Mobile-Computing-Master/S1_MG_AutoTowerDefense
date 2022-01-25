using System.Collections.Generic;
using Core.Enums;
using Turrets;
using UnityEngine;

namespace Core.Game
{
    public class TurretRepository: MonoBehaviour
    {
        private readonly List<GameObject> tier1 = new List<GameObject>();
        private readonly List<GameObject> tier2 = new List<GameObject>();
        private readonly List<GameObject> tier3 = new List<GameObject>();
        private const int MaxNumberOfSameTurret = 2;

        public void AddTurret(GameObject go)
        {
            var turretBase = go.GetComponent<TurretBase>();

            if (turretBase.tier != TurretTier.Tier1) return;

            var t1Siblings = tier1.FindAll(t => t.GetComponent<TurretBase>().Type == turretBase.Type);

            if (t1Siblings.Count < 2)
            {
                tier1.Add(go);
            }
            else
            {
                UpgradeToTier2(t1Siblings, turretBase, go);
            }
        }

        private void UpgradeToTier2(List<GameObject> t1Siblings, TurretBase turretBase, GameObject go)
        {
            RemoveTurretsFromListAndDestroy(tier1, t1Siblings);
            
            turretBase.tier++;

            var t2Siblings = tier2.FindAll(t => t.GetComponent<TurretBase>().Type == turretBase.Type);

            if (t2Siblings.Count == 2)
            {
                UpgradeToTier3(t2Siblings, turretBase, go);
            }
            else
            {
                tier2.Add(go);
            }
        }

        private void UpgradeToTier3(List<GameObject> t2Siblings, TurretBase turretBase, GameObject go)
        {
            RemoveTurretsFromListAndDestroy(tier2, t2Siblings);

            turretBase.tier++;
            tier3.Add(go);
        }

        private void RemoveTurretsFromListAndDestroy(List<GameObject> list, List<GameObject> toRemove)
        {
            toRemove.ForEach(n =>
            {
                list.Remove(n);
                Destroy(n);
            });
        }
    }
}