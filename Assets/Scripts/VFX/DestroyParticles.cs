using System;
using UnityEngine;

namespace VFX
{
    public class DestroyParticles: MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().main.duration);
        }
    }
}