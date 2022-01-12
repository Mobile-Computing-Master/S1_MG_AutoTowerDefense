using System;
using System.Collections.Generic;
using Core.Map;
using UnityEngine;
using Zenject;

namespace Path
{
    public class PathMap : MonoBehaviour
    {
        public Vector3 player1Base;
        public Vector3 player2Base;
        public List<Vector3> points = new List<Vector3>();
        public float pathWidth = 1.0f;
        
        private MapManager _mapManager;

        private void Start()
        {
            Initiate();
            _mapManager.InitiateProtectedSpace();
        }
        
        private void Initiate()
        {
            var sceneContext = GameObject.Find("Context");
            _mapManager = sceneContext.GetComponent<MapManager>();
        }
    }
    
}
