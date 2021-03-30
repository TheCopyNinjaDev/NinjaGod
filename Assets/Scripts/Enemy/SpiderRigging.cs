using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRigging : MonoBehaviour
{
    public LayerMask ground;

    public GameObject body;
    public Transform target;
    public Transform groundTarget;
    public Transform airTarget;
    public Transform tip;
    public GameObject oppositeLeg;
    public float speed;

    private Vector3 targetPos;
    [HideInInspector]
    public bool isGrounded = true;

    private void Start()
    {
        // The start position of the main target
        targetPos = target.transform.position;
    }

    private void LateUpdate()
    {
        // Fixates the target to needed coordinates
        target.position = targetPos;

        // Raycast on the ground to know where to put leg
        Vector3 raycastOrigin = airTarget.position;
        Ray down = new Ray(raycastOrigin, -transform.up);
        Physics.Raycast(down, out var hit, 10f, ground);
        groundTarget.position = hit.point;

        float distance = Vector3.Distance(groundTarget.position, tip.position);

        float step = speed * Time.deltaTime;
        Vector3 neededPos = new Vector3(groundTarget.position.x, groundTarget.position.y + 2f, groundTarget.position.z);
        // Step
        if ((distance >= 1.5f || !isGrounded) && oppositeLeg.GetComponent<SpiderRigging>().isGrounded)
        {
            isGrounded = false;
            targetPos = Vector3.MoveTowards(targetPos, neededPos, step);
        }
        if (targetPos == neededPos)
        {
            isGrounded = true;
        }

        // Body Y position and rotations based on legs position
        body.transform.position = new Vector3(body.transform.position.x, LegsInfo.avgLegsY + 1.5f, body.transform.position.z);
        Quaternion parentRot = GetComponentInParent<Transform>().localRotation;
        body.transform.localRotation = Quaternion.Euler(LegsInfo.legsHeightDiffX * -10, parentRot.y + 180, LegsInfo.legsHeightDiffZ * -10);
        
    }
}
