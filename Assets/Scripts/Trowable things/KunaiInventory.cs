using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trowable_things
{
    public class KunaiInventory: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textUsual;
        
        [SerializeField] private int quantityUsual = 3;
        [SerializeField] private int quantityTeleport = 3;
        [SerializeField] private int quantityMissile = 3;
        [SerializeField] private int quantityExplosive = 3;
        

        private void Update()
        {
            textUsual.text = "x" + quantityUsual;
        }

        public void SpendKunai(int kunai)
        {
            switch (kunai)
            {
                case 0:
                    quantityUsual--;
                    break;
            }
        }
    }
}