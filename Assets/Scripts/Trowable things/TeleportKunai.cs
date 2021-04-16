using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Trowable_things
{
    public class TeleportKunai: Kunai
    {
        private Transform _player;
        private bool _readyToTeleport;
        protected override void Awake()
        {
            base.Awake();
            _player = GameObject.FindWithTag("Player").transform;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            if (ReadyToStick)
            {
                Rb.constraints  = RigidbodyConstraints.FreezeAll;
                _readyToTeleport = true;
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject, 1);
            }
        }

        private void LateUpdate()
        {
            // Teleports player to the last thrown kunai
            if (Input.GetKey("t") && _readyToTeleport)
            {
                var position = FightSystem.CurrentKunai.transform.position;
                _player.position = new Vector3(position.x, 
                    position.y + 2, position.z);
            }
        }
    }
}