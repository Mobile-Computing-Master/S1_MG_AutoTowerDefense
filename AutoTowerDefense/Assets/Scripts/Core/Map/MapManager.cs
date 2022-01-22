using System;
using System.Collections.Generic;
using System.Linq;
using Path;
using UnityEngine;

namespace Core.Map
{
    public class MapManager : MonoBehaviour
    {
        private readonly List<Tuple<Vector3, Vector3>> _protectedSpaceLines = new List<Tuple<Vector3, Vector3>>();
        private float _pathWidth;
        
        public void InitiateProtectedSpace()
        {
            var pathMap = GameObject.Find("Path").GetComponent<PathMap>();
            _pathWidth = pathMap.pathWidth;
            
            // Create a list with lines, that indicate the path
            _protectedSpaceLines.Add(new Tuple<Vector3, Vector3>(pathMap.creepSpawnPoint, pathMap.points[0]));

            for (int i = 0; i < pathMap.points.Count - 1; i++)
            {
                _protectedSpaceLines.Add(new Tuple<Vector3, Vector3>(pathMap.points[i], pathMap.points[i + 1]));
            }
            
            _protectedSpaceLines.Add(new Tuple<Vector3, Vector3>(pathMap.points.Last(), pathMap.playerBasePoint));
        }

        public bool IsInProtectedSpace(Vector3 vector)
        {
            var isInProtectedSpace = false;
            
            foreach (var line in _protectedSpaceLines)
            {
                var distance = DistanceToLine(line.Item1, line.Item2, vector);
                if (distance <= _pathWidth)
                {
                    isInProtectedSpace = true;
                    break;
                }
            }

            return isInProtectedSpace;
        }
        
        private static float DistanceToLine(Vector3 start, Vector3 end, Vector3 pnt)
        {
            var ax = start.x;
            var ay = start.y;
            var bx = end.x;
            var by = end.y;
            var x = pnt.x;
            var y = pnt.y;
            
            if ((ax-bx)*(x-bx)+(ay-by)*(y-by) <= 0)
                return (float)Math.Sqrt((x - bx) * (x - bx) + (y - by) * (y - by));

            if ((bx-ax)*(x-ax)+(by-ay)*(y-ay) <= 0)
                return (float)Math.Sqrt((x - ax) * (x - ax) + (y - ay) * (y - ay));

            return (float)(Math.Abs((by - ay)*x - (bx - ax)*y + bx*ay - by*ax) /
                   Math.Sqrt((ay - by) * (ay - by) + (ax - bx) * (ax - bx)));
        }
    }
}