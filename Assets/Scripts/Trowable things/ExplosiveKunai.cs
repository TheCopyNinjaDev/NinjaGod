using Trowable_things;
using UnityEngine;

public class ExplosiveKunai : Kunai
{
    [SerializeField] private float _explosionRadius = 10;
    
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        var things = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var thing in things)
        {
            if(thing.GetComponent<Rigidbody>())
                thing.GetComponent<Rigidbody>().AddExplosionForce(500, transform.position, _explosionRadius);
        }
    }
}
