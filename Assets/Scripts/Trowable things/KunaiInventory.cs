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

        protected internal static int QuantityUsual = 3;
        protected internal static int QuantityTeleport = 3;
        protected internal static int QuantityMissile = 3;
        protected internal static int QuantityExplosive = 3;



        private void Update()
        {
            textUsual.text = "x" + QuantityUsual;
            textTeleport.text = "x" + QuantityTeleport;
            textMissile.text = "x" + QuantityMissile;
            textExplosive.text = "x" + QuantityExplosive;
            
            /*[Temporary zone]*/
            var selectedKunai = CirculasMenu.CurMenuItem;
            kunaiQuantity.text = selectedKunai switch
            {
                0 => "x" + QuantityUsual,
                1 => "x" + QuantityTeleport,
                2 => "x" + QuantityMissile,
                3 => "x" + QuantityExplosive,
                _ => kunaiQuantity.text
            };
        }

        public static void SpendKunai(int kunai)
        {
            switch (kunai)
            {
                case 0:
                    QuantityUsual--;
                    break;
                case 1:
                    QuantityTeleport--;
                    break;
                case 2:
                    QuantityMissile--;
                    break;
                case 3:
                    QuantityExplosive--;
                    break;
            }
        }
    }
}