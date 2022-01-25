using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using UnityEngine;

namespace Turrets
{
    public class DamageBuffTurret : TurretBase
    {
        public new TurretType type = TurretType.DamageBuff;
        private List<GameObject> _buffed = new List<GameObject>();

        public override void BuyUpgrade()
        {
            throw new System.NotImplementedException();
        }

        private void Update()
        {
            var x = gameObject.GetComponent<CircleCollider2D>();
        }
    }
}