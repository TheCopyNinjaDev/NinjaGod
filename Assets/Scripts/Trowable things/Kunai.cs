using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private Rigidbody _rb;
    private FixedJoint _fixedJoint;
    private bool _readyToStick = false;
    public bool _readyToTeleport;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _readyToStick = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _readyToStick = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_readyToStick)
        {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            _readyToTeleport = true;
        }
    }

}
