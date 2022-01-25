using Core.Enums;

namespace Turrets
{
    public class ReloadBuffTurret : TurretBase
    {
        public override TurretType Type { get; protected set; } = TurretType.ReloadBuff;
    }
}