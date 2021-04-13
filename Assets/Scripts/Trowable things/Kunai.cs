using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Trowable_things
{
    public class Kunai : MonoBehaviour
    {
        [FormerlySerializedAs("_readyToTeleport")] [HideInInspector]


        public float speed = 100;

        protected Rigidbody _rb;
        protected bool _readyToStick = false;
    

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {
            _rb.AddRelativeForce(Vector3.up * speed);
        }


        protected virtual void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                _readyToStick = true;
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _readyToStick = false;
            }
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (_readyToStick)
            {
                _rb.constraints  = RigidbodyConstraints.FreezeAll;
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject, 1);
            }
        }
    }
}
