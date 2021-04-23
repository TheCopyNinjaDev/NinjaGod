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
        
        [SerializeField] private int quantityUsual = 3;
        [SerializeField] private int quantityTeleport = 3;
        [SerializeField] private int quantityMissile = 3;
        [SerializeField] private int quantityExplosive = 3;
        

        private void Update()
        {
            textUsual.text = "x" + quantityUsual;
            textTeleport.text = "x" + quantityTeleport;
            textMissile.text = "x" + quantityMissile;
            textExplosive.text = "x" + quantityExplosive;
        }

        public void SpendKunai(int kunai)
        {
            switch (kunai)
            {
                case 0:
                    quantityUsual--;
                    break;
                case 1:
                    quantityTeleport--;
                    break;
                case 2:
                    quantityMissile--;
                    break;
                case 3:
                    quantityExplosive--;
                    break;
            }
        }
    }
}