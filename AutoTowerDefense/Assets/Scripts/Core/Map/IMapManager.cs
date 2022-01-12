using UnityEngine;

namespace Core.Map
{
    public interface IMapManager
    {
        public void InitiateProtectedSpace();
        public bool IsInProtectedSpace(Vector3 vector);
    }
}