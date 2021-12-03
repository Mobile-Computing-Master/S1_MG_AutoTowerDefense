using UnityEngine;

namespace Core
{
    public class Utils : MonoBehaviour
    {
        public static Vector2 ScreenToWorld(Camera camera, Vector2 position)
        {
            Physics.Raycast(camera.ScreenPointToRay(position), out var hit);
                return hit.point;
        }

        public static float VectorXYSum(Vector3 vector)
        {
            return vector.x + vector.y;
        }
    }
}
