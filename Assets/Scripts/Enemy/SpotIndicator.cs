using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotIndicator : MonoBehaviour
{
    [SerializeField]
    private Image spotSign;


    public void FillTheSign(float fillingSpeed)
    {
        if(spotSign.fillAmount < 1.0f)
            spotSign.fillAmount += fillingSpeed * Time.deltaTime; 
    }

    public bool isSpotted()
    {
        if(spotSign.fillAmount == 1.0f)
            return true;
        else
            return false;
    }
}
