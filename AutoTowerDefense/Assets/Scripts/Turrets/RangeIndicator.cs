using System;
using System.Collections;
using System.Collections.Generic;
using Turrets;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    private void OnEnable()
    {
        var turretBase = gameObject.GetComponentInParent<TurretBase>();

        if (!turretBase) throw new Exception("Attach a turret base!");
        
        // Set indicator size according to range
        gameObject.transform.localScale = new Vector3(turretBase.range * 2, turretBase.range * 2, 0);
    }
}
