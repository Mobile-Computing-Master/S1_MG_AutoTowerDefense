﻿using Core.Enums;

namespace Turrets
{
    public class MoneyFarmTurret : TurretBase
    {
        public new TurretType type = TurretType.MoneyFarm;
        
        public override void BuyUpgrade()
        {
            throw new System.NotImplementedException();
        }
    }
}