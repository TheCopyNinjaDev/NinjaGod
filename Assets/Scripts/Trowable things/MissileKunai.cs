using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trowable_things
{
    public class MissileKunai : Kunai
    {
        private GameObject[] _enemies;
        private Transform _targetedEnemy;
        [SerializeField] private float missileSpeed = 10;
        
        protected override void Start()
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            if(collision.collider.CompareTag("Enemy"))
                Debug.Log("Hit");
        }

        protected override void FixedUpdate()
        {
            if (_enemies.Length > 0)
            {
                var targetedEnemyindex = Random.Range(0, _enemies.Length);
                _targetedEnemy = _enemies[targetedEnemyindex].transform;
                var direction = _targetedEnemy.position - transform.position;
                if (Vector3.Angle(transform.forward, direction) < 90)
                {
                    Rb.useGravity = false;
                    Rb.AddForce(direction * missileSpeed);
                }
                else
                {
                    base.FixedUpdate();
                }
            }
            
            else
            {
                base.FixedUpdate();
            }
        }
    }
}

