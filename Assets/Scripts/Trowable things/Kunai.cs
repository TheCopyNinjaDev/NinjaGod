using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Trowable_things
{
    public class Kunai : MonoBehaviour
    {


        public float speed = 100;

        protected Rigidbody Rb;
        protected bool ReadyToStick = false;
    

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {
            Rb.AddRelativeForce(Vector3.up * speed);
        }


        protected virtual void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                ReadyToStick = true;
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ReadyToStick = false;
            }
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (ReadyToStick)
            {
                Rb.constraints  = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
