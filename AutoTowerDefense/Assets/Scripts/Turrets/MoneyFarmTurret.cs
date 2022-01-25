using Core.Enums;

namespace Turrets
{
    public class MoneyFarmTurret : TurretBase
    {
        public override TurretType Type { get; protected set; } = TurretType.MoneyFarm;
    }
}