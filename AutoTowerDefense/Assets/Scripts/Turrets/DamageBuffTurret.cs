using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Turrets
{
    public class DamageBuffTurret : TurretBase
    {
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