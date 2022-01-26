using System;
using Core.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Components
{
    public class RerollButton : MonoBehaviour
    {
        private BankService _bankService;
        private TurretRoller _turretRoller;
        private readonly Color _shade = new Color(0.4f, 0.4f, 0.4f, 1);
        private readonly Color _noShade = new Color(1, 1, 1, 1);

        private void OnEnable()
        {
            Initiate();
            
            _bankService.OnBalanceChanged += UpdateColor;
        }

        private void UpdateColor(uint balance)
        {
            if (_turretRoller.rerollCosts > balance)
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
            _bankService = context.GetComponent<BankService>();
            _turretRoller = context.GetComponent<TurretRoller>();
        }
    }
}