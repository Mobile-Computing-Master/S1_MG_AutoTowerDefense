using System;
using System.Collections.Generic;
using Core.Enums;

namespace Turrets.Utils
{
    public static class TurretPrices
    {
        private static readonly Dictionary<TurretType, uint> _priceMap = new Dictionary<TurretType, uint>()
        {
            { TurretType.None, 0 },
            { TurretType.Basic, 4 },
            { TurretType.Multi, 6 },
            { TurretType.Freeze, 12 },
            { TurretType.Moab, 7 },
            { TurretType.Sniper, 10 },
            { TurretType.Hazard, 6 },
            { TurretType.DamageBuff, 5 },
            { TurretType.ReloadBuff, 5 },
            { TurretType.MoneyFarm, 9 }
        };

        public static uint GetPriceByTurretType(TurretType type)
        {
            if (!_priceMap.TryGetValue(type, out var price))
            {
                throw new Exception($"Could not get price of {type}");
            }

            return price;
        }
    }
}