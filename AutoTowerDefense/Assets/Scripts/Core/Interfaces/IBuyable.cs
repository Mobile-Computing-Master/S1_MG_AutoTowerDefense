using UnityEngine;

namespace Core.Interfaces
{
    public interface IBuyable
    {
        public int Buy();
        public void StartBuyPreview();
        public void EndBuyPreview();

        public void UpdatePreview(Vector3 position);
    }
}