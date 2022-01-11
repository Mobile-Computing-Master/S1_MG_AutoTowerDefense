namespace Core.Interfaces
{
    public interface IBuyable
    {
        public int Buy();
        public void StartBuyPreview();
        public void EndBuyPreview();
    }
}