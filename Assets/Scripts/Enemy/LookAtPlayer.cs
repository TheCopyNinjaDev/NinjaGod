using System;
using UnityEngine;

namespace Enemy
{
    public class LookAtPlayer: MonoBehaviour
    {
        private Transform _playerPos;
        
        private void Start()
        {
            _playerPos = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            var dir = _playerPos.position - transform.position;
            // When player is not in attack zone npc need to rotate to him
            if (Vector3.Angle(dir, transform.forward) > 45)
            {
                
            }
        }
    }
}