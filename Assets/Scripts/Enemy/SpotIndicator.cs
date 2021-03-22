using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotIndicator : MonoBehaviour
{
    [SerializeField]
    private Image spotSign;

    private bool isTriggered;

    private void Update() 
    {
        if(spotSign.fillAmount > 0 && spotSign.fillAmount < 1.0f)
        {
            spotSign.color = Color.yellow;
        }
        else if(spotSign.fillAmount == 1.0f)
        {
            spotSign.color = Color.red;
        }    
    }
    public bool IsTriggered
    {
        get{ return isTriggered; }
        set
        {
            if(spotSign.fillAmount > 0 && spotSign.fillAmount < 1.0f)
                isTriggered = true;
            else
                isTriggered = false;
        }
    }

    public bool IsSpotted()
    {
            if(spotSign.fillAmount == 1.0f)
                return true;
            else
                return false;
    }

    public void FillTheSign(float fillingSpeed)
    {
        if(spotSign.fillAmount < 1.0f)
            spotSign.fillAmount += fillingSpeed * Time.deltaTime; 
    }

    public void UnfillTheSign(float unfillingSpeed)
    {
        if(spotSign.fillAmount > 0)
            spotSign.fillAmount -= unfillingSpeed * Time.deltaTime;
    }
}
