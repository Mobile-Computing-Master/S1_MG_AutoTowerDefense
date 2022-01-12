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
            _protectedSpaceLines.Add(new Tuple<Vector3, Vector3>(pathMap.player1Base, pathMap.points[0]));

            for (int i = 0; i < pathMap.points.Count - 1; i++)
            {
                _protectedSpaceLines.Add(new Tuple<Vector3, Vector3>(pathMap.points[i], pathMap.points[i + 1]));
            }
            
            _protectedSpaceLines.Add(new Tuple<Vector3, Vector3>(pathMap.points.Last(), pathMap.player2Base));
        }

        public bool IsInProtectedSpace(Vector3 vector)
        {
            var isInProtectedSpace = false;
            
            foreach (var line in _protectedSpaceLines)
            {
                var distance = DistanceToLine(line.Item1, line.Item2, vector);

                if (distance.x <= _pathWidth || distance.y <= _pathWidth)
                {
                    isInProtectedSpace = true;
                    break;
                }
            }

            return isInProtectedSpace;
        }
        
        private static Vector3 DistanceToLine(Vector3 start, Vector3 end, Vector3 pnt)
        {
            var line = (end - start);
            var len = line.magnitude;
            line.Normalize();
   
            var v = pnt - start;
            var d = Vector3.Dot(v, line);
            d = Mathf.Clamp(d, 0f, len);
            return start + line * d;
        }
    }
}