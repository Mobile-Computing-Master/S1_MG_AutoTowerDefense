using System.Collections;
using System.Collections.Generic;
using Core.GameManager;
using Turrets;
using UnityEngine;

public class LocalGameManager : ILocalGameManager
{
    private TurretBase _selectedTurret = null;
    
    public void SetSelectedTurret(TurretBase turret)
    {
        _selectedTurret = turret;
    }
}
