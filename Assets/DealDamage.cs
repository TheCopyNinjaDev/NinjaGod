using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public void Deal()
    {
        GameObject.FindGameObjectWithTag("Player").SendMessage("DealDamage");
    }
}
