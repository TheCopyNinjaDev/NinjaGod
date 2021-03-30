using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpotIndicator : MonoBehaviour
{
    [SerializeField]
    private Image spotSign;

    private void Update()
    {
        if (spotSign.fillAmount > 0 && spotSign.fillAmount < 1.0f)
        {
            spotSign.color = Color.yellow;
        }
        else if (spotSign.fillAmount == 1.0f)
        {
            spotSign.color = Color.red;
        }
    }
    public bool isUnnoticed()
    {
        return spotSign.fillAmount == 0;
    }
    public bool IsTriggered()
    {
        return spotSign.fillAmount >= 0.5 && spotSign.fillAmount < 1.0f;
    }

    public bool IsSpotted()
    {
        return spotSign.fillAmount == 1.0f;
    }

    public void FillTheSign(float fillingSpeed)
    {
        if (spotSign.fillAmount < 1.0f)
            spotSign.fillAmount += fillingSpeed * Time.deltaTime;
    }

    public void UnfillTheSign(float unfillingSpeed)
    {
        if (spotSign.fillAmount > 0)
            spotSign.fillAmount -= unfillingSpeed * Time.deltaTime;
    }
}
