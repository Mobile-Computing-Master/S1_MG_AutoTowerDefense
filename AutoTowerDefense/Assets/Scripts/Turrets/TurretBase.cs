using System;
using UnityEngine;

namespace Turrets
{
    public abstract class TurretBase : MonoBehaviour
    {
        public float range = 4f;
        private bool _rangeIndicatorActive = false;
    }
}
