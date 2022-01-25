﻿using Core.Enums;
using Mobs;
using UnityEngine;

namespace Turrets
{
    public class HazardTurret : TurretBase
    {
        public new TurretType type = TurretType.Hazard;
        public float damage = 5;
        
        public override void BuyUpgrade()
        {
            throw new System.NotImplementedException();
        }
        
        public void Update()
        {
            if (!active) return;

            ReloadTime += Time.deltaTime;
            if (InRange.Count <= 0 || !(ReloadTime >= 1 / hitsPerSecond)) return;
            
            foreach (var enemy in InRange)
            {
                Shoot(enemy);
            }
            ReloadTime = 0;
        }

        protected override void Shoot(GameObject target)
        {
            target.GetComponent<Creep>().hp -= damage;
        }
    }
}