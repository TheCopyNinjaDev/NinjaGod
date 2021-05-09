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
            if (Vector3.Angle(dir, transform.forward) > 45)
            {
                Vector3.RotateTowards(transform.forward, dir, 30, 30);
            }
        }
    }
}