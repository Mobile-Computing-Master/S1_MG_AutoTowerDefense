using UnityEngine;

namespace Core.UI
{
    public interface IUiController
    {
        public void OpenMainSideDrawer();
        public void CloseMainSideDrawer();

        public bool ToggleMainSideDrawer();

        public void OpenTurretConfirmPopover(Vector3 position);
        public void CloseTurretConfirmPopover();
        
    }
}
