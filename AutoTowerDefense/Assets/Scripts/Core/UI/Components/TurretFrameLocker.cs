using System;
using System.Collections.Generic;
using System.Linq;
using Core.Game;
using Turrets;
using Turrets.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Components
{
    public class TurretFrameLocker : MonoBehaviour
    {
        public int slot = 0;
        private bool _alreadyBought = false;
        private TurretRoller _turretRoller;
        private BankService _bankService;
        private readonly Color _shade = new Color(0.4f, 0.4f, 0.4f, 1);
        private readonly Color _noShade = new Color(1, 1, 1, 1);
        private string CoinName = "Coins";

        private void OnEnable()
        {
            Initiate();
            
            _turretRoller.OnRollChanged += TurretRollerOnOnRollChanged;
            _bankService.OnBalanceChanged += BankServiceOnBalanceChanged;
        }

        public void SetAlreadyBought(bool bought)
        {
            _alreadyBought = bought;

            gameObject.GetComponentInChildren<Text>().enabled = !_alreadyBought;
            gameObject.GetComponentsInChildren<Image>().First(i => i.name == CoinName).enabled = !_alreadyBought;

            if (_alreadyBought)
            {
                gameObject.GetComponent<Image>().color = _noShade;
            }
        }
        
        public bool GetAlreadyBought()
        {
            return _alreadyBought;
        }
        
        private void TurretRollerOnOnRollChanged(List<GameObject> turretPrefabs)
        {
            BankServiceOnBalanceChanged(_bankService.GetBalance());
        }
        
        private void BankServiceOnBalanceChanged(uint balance)
        {
            var price = TurretPrices.GetPriceByTurretType(_turretRoller.GetTurretPrefabBySlot(slot).GetComponent<TurretBase>().Type);

            if (price > balance && !_alreadyBought)
            {
                gameObject.GetComponent<Image>().color = _shade;
            }
            else
            {
                gameObject.GetComponent<Image>().color = _noShade;

            }
        }

        private void Initiate()
        {
            var context = GameObject.Find("Context");
            _turretRoller = context.GetComponent<TurretRoller>();
            _bankService = context.GetComponent<BankService>();
        }
    }
}