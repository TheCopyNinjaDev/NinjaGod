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

    private Vector3 _targetPos;
    [HideInInspector]
    public bool isGrounded = true;

    private void Start()
    {
        // The start position of the main target
        _targetPos = target.transform.position;
    }

    private void LateUpdate()
    {
        // Fixates the target to needed coordinates
        target.position = _targetPos;

        // Raycast on the ground to know where to put leg
        Vector3 raycastOrigin = airTarget.position;
        Ray down = new Ray(raycastOrigin, -transform.up);
        Physics.Raycast(down, out var hit, 10f, ground);
        Vector3 position1;
        position1 = hit.point;
        groundTarget.position = position1;

        float distance = Vector3.Distance(position1, tip.position);

        float step = speed * Time.deltaTime;
        Vector3 neededPos = new Vector3(position1.x, position1.y + 2f, position1.z);
        // Step
        if ((distance >= 1.5f || !isGrounded) && oppositeLeg.GetComponent<SpiderRigging>().isGrounded)
        {
            isGrounded = false;
            _targetPos = Vector3.MoveTowards(_targetPos, neededPos, step);
        }
        if (_targetPos == neededPos)
        {
            isGrounded = true;
        }

        // Body Y position and rotations based on legs position
        var position = body.transform.position;
        position = new Vector3(position.x, LegsInfo.avgLegsY + 1.5f, position.z);
        body.transform.position = position;
        Quaternion parentRot = GetComponentInParent<Transform>().localRotation;
        body.transform.localRotation = Quaternion.Euler(LegsInfo.legsHeightDiffX * -10, parentRot.y + 180, LegsInfo.legsHeightDiffZ * -10);
        
    }
}
