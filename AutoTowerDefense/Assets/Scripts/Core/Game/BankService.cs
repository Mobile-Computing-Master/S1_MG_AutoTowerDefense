using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Game
{
    public class BankService : MonoBehaviour
    {
        private uint _balance = 50;

        private GameObject _balanceDisplay;
        private const string BalanceDisplayName = "coin_amount";

        private void Start()
        {
            Initiate();
            UpdateBalanceDisplay();
        }

        public uint GetBalance()
        {
            return _balance;
        }

        public void Add(uint amount)
        {
            _balance += amount;
            UpdateBalanceDisplay();
        }

        public bool TryWithdraw(uint amount)
        {

            try
            {
                _balance -= amount;
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
            return _balance >= amount;
        }
        
        private void UpdateBalanceDisplay()
        {
            _balanceDisplay.GetComponent<Text>().text = _balance.ToString();
        }
        
        private void Initiate()
        {
            _balanceDisplay = GameObject.Find(BalanceDisplayName);
        }
    }
}