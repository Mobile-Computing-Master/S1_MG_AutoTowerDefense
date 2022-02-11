using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Game
{
    public class BankService : MonoBehaviour
    {
        public delegate void BalanceChanged(uint balance);
        public event BalanceChanged OnBalanceChanged;
        
        public uint balance = 20;

        private GameObject _balanceDisplay;
        private const string BalanceDisplayName = "coin_amount";

        private void Start()
        {
            Initiate();
            UpdateBalanceDisplay();
        }

        public uint GetBalance()
        {
            return balance;
        }

        public void Add(uint amount)
        {
            balance += amount;
            UpdateBalanceDisplay();
        }

        public bool TryWithdraw(uint amount)
        {

            try
            {
                balance -= amount;
                UpdateBalanceDisplay();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CanAfford(uint amount)
        {
            return balance >= amount;
        }
        
        private void UpdateBalanceDisplay()
        {
            OnBalanceChanged?.Invoke(balance);
            _balanceDisplay.GetComponent<Text>().text = balance.ToString();
        }
        
        private void Initiate()
        {
            _balanceDisplay = GameObject.Find(BalanceDisplayName);
        }
    }
}