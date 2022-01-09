namespace Core.UI
{
    public interface IUiController
    {
        public void OpenMainSideDrawer();
        public void CloseMainSideDrawer();

        public bool ToggleMainSideDrawer();
    }
}
