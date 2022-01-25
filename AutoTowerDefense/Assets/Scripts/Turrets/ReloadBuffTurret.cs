using Core.Enums;

namespace Turrets
{
    public class ReloadBuffTurret : TurretBase
    {
        public new TurretType type = TurretType.ReloadBuff;
        
        public override void BuyUpgrade()
        {
            throw new System.NotImplementedException();
        }
    }
}