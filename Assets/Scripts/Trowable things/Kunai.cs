using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [HideInInspector]
    public bool _readyToTeleport;

    public float speed = 100;

    private Rigidbody _rb;
    private bool _readyToStick = false;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.AddRelativeForce(Vector3.up * speed);
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
           _rb.constraints  = RigidbodyConstraints.FreezeAll;
            _readyToTeleport = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject, 1);
        }
    }

}
