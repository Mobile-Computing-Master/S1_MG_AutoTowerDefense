using Turrets;

namespace Core.GameManager
{
    public interface ILocalGameManager
    {
        public void SetSelectedTurret(TurretBase turret);
        public void StartUiElementDrag();
        public void EndUiElementDrag();

        public bool UiElementIsDragged();
    }
}