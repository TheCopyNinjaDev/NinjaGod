using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Trowable_things
{
    public class ExplosiveKunai : Kunai
    {
        [FormerlySerializedAs("_explosionRadius")] [SerializeField] private float explosionRadius = 10;
        [FormerlySerializedAs("_explosionForce")] [SerializeField] private float explosionForce = 500;
        [FormerlySerializedAs("_explosion")] [SerializeField] private ParticleSystem explosion;
        
        protected override void Start()
        {
            KunaiInventory.SpendKunai(3);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            if (!ReadyToStick) return;
            Rb.constraints  = RigidbodyConstraints.FreezeAll;
            var things = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var thing in things.Where(thing => thing.GetComponent<Rigidbody>()))
            {
                thing.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
