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
            { TurretType.Basic, 1 },
            { TurretType.Multi, 2 },
            { TurretType.Freeze, 3 },
            { TurretType.Moab, 4 },
            { TurretType.Sniper, 5 },
            { TurretType.Hazard, 6 },
            { TurretType.DamageBuff, 7 },
            { TurretType.ReloadBuff, 8 },
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