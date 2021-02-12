using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsInfo : MonoBehaviour
{
    static public float avgLegsY;
    static public float legsHeightDiffZ;
    static public float legsHeightDiffX;

    public Transform BR;
    public Transform BL;
    public Transform FR;
    public Transform FL;

    private void Update()
    {
        avgLegsY = (FL.position.y + FR.position.y + BL.position.y + BR.position.y) / 4;

        /*legsHeightDiff = Mathf.Acos(Vector3.Scale((FL.position - FR.position), (FL.position - BL.position)).magnitude
            / ((FL.position - FR.position).magnitude * (FL.position - BL.position).magnitude));
        print(legsHeightDiff);*/

        legsHeightDiffZ = ((FL.position.y + BL.position.y) - (FR.position.y + BR.position.y)) / 2;
        legsHeightDiffX = ((FL.position.y + FR.position.y) - (BR.position.y + BL.position.y)) / 2;
    }
}
