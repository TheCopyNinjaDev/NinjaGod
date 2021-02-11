using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsInfo : MonoBehaviour
{
    static public float avgLegsY;
    static public float legsHeightDiff;

    public Transform FL;
    public Transform FR;
    public Transform BL;
    public Transform BR;

    private void Update()
    {
        avgLegsY = (FL.position.y + FR.position.y + BL.position.y + BR.position.y) / 4;
    }
}
