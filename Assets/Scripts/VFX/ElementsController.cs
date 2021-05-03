using System;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class ElementsController: MonoBehaviour
    {
        [SerializeField] private ParticleSystem fireSpit;
        private void Update()
        {
            if (Input.GetKey("e"))
            {
                fireSpit.Play();
            }
        }
    }
}