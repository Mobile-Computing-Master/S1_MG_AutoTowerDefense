using Turrets;

namespace Core.GameManager
{
    public class LocalGameManager : ILocalGameManager
    {
        private TurretBase _selectedTurret = null;
        private bool _uiElementIsDragged = false;
    
        public void SetSelectedTurret(TurretBase turret)
        {
            _selectedTurret = turret;
        }

        public void StartUiElementDrag()
        {
            _uiElementIsDragged = true;
        }

        public void EndUiElementDrag()
        {
            _uiElementIsDragged = false;
        }

        public bool UiElementIsDragged()
        {
            return _uiElementIsDragged;
        }
    }
}
