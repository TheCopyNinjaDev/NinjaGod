using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trowable_things
{
    public class KunaiInventory: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textUsual;
        [SerializeField] private TextMeshProUGUI textTeleport;
        [SerializeField] private TextMeshProUGUI textMissile;
        [SerializeField] private TextMeshProUGUI textExplosive;
        /*[Temporary zone]*/
        [SerializeField] private TextMeshProUGUI kunaiQuantity;

        private static int _quantityUsual = 3;
        private static int _quantityTeleport = 3;
        private static int _quantityMissile = 3;
        private static int _quantityExplosive = 3;



        private void Update()
        {
            textUsual.text = "x" + _quantityUsual;
            textTeleport.text = "x" + _quantityTeleport;
            textMissile.text = "x" + _quantityMissile;
            textExplosive.text = "x" + _quantityExplosive;
            
            /*[Temporary zone]*/
            var selectedKunai = CirculasMenu.CurMenuItem;
            kunaiQuantity.text = selectedKunai switch
            {
                0 => "x" + _quantityUsual,
                1 => "x" + _quantityTeleport,
                2 => "x" + _quantityMissile,
                3 => "x" + _quantityExplosive,
                _ => kunaiQuantity.text
            };
        }

        public static void SpendKunai(int kunai)
        {
            switch (kunai)
            {
                case 0:
                    _quantityUsual--;
                    break;
                case 1:
                    _quantityTeleport--;
                    break;
                case 2:
                    _quantityMissile--;
                    break;
                case 3:
                    _quantityExplosive--;
                    break;
            }
        }
    }
}