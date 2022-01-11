using System;
using UnityEngine;

namespace Turrets
{
    public class RangeIndicator : MonoBehaviour
    {
        private void Start()
        {
            var turretBase = gameObject.GetComponentInParent<TurretBase>();

            if (!turretBase) throw new Exception("Attach a turret base!");
        
            // Set indicator size according to range
            gameObject.transform.localScale = new Vector3(turretBase.range * 2, turretBase.range * 2, 0);

            turretBase.OnTurretSelected += ChangeVisibility;
        }

        private void ChangeVisibility(bool selected)
        {
            Debug.Log("Change visiblity: " + selected);
            gameObject.SetActive(selected);
        }
    }
}
