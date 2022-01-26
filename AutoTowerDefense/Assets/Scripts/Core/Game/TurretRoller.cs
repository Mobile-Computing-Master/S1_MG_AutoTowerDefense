﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Core.Enums;
using Core.UI.Components;
using Turrets;
using UnityEngine;
using Random = System.Random;

namespace Core.Game
{
    public class TurretRoller : MonoBehaviour
    {
        public int numberOfSlots = 3;
        public List<GameObject> turretPrefabs = new List<GameObject>();

        public delegate void RollChange(List<GameObject> turretPrefabs);

        public event RollChange OnRollChanged;

        public uint rerollCosts = 2;
        
        private readonly List<GameObject>rolledTurrets = new List<GameObject>() {null, null, null};
        private const string TurretFrameName = "turretFrame_";

        private void Start()
        {
            if (turretPrefabs.Count < 8) throw new Exception("Add at all turrets to turret roller before starting");
            
            RollTurrets();
        }

        public GameObject GetTurretPrefabBySlot(int slot)
        {
            if (slot > numberOfSlots) throw new Exception("Slots out of bounds");
            
            return rolledTurrets[slot];
        }
        
        public void RollTurrets()
        {
            Random r = new Random();

            for (var i = 0; i < numberOfSlots; i++)
            {
                rolledTurrets[i] = turretPrefabs[r.Next(0, turretPrefabs.Count)];
            }
            
            OnRollChanged?.Invoke(rolledTurrets);
        }
        
        public void ResetTurretSlots()
        {
            for (int i = 0; i <numberOfSlots; i++)
            {
                GameObject.Find($"{TurretFrameName}{i}").GetComponent<TurretFrameLocker>().SetAlreadyBought(false);
            }
        }
    }
}