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
        
        protected override void Start()
        {
            base.Start();
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
            var targetedEnemyindex = Random.Range(0, _enemies.Length);
            _targetedEnemy = _enemies[targetedEnemyindex].transform;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            if(collision.collider.CompareTag("Enemy"))
                Debug.Log("Hited");
        }

        private void FixedUpdate()
        {
            Vector3.MoveTowards(transform.position, _targetedEnemy.position, speed * Time.deltaTime);
        }
    }
}

