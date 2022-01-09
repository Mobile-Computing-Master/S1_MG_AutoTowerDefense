using System;
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

        public static bool IsInsideRect(Vector3 position, Rect rectangle)
        {
            return false;
        }

        public static Vector3 SnapToGrid(Vector3 vector)
        {
            vector.x = (float) Math.Round(vector.x * 2, MidpointRounding.AwayFromZero) / 2;
            vector.y = (float) Math.Round(vector.y * 2, MidpointRounding.AwayFromZero) / 2;

            return vector;
        }
    }
}
