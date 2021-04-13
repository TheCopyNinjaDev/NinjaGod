using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Trowable_things
{
    public class TeleportKunai: Kunai
    {
        private Transform _player;
        protected override void Awake()
        {
            base.Awake();
            _player = GameObject.FindWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            // Teleports player to the last thrown kunai
            if (Input.GetKey("t") && FightSystem.CurrentKunai.gameObject.GetComponentInChildren<Kunai>().readyToTeleport)
            {
                var position = FightSystem.CurrentKunai.transform.position;
                _player.position = new Vector3(position.x, 
                    position.y + 2, position.z);
            }
        }
    }
}