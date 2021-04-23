using Trowable_things;
using UnityEngine;

public class ExplosiveKunai : Kunai
{
    [SerializeField] private float _explosionRadius = 10;
    [SerializeField] private float _explosionForce = 500;
    [SerializeField] private ParticleSystem _explosion;

    protected override void Start()
    {
        GameObject.Find("GUI").GetComponent<KunaiInventory>().SpendKunai(3);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (!ReadyToStick) return;
        Rb.constraints  = RigidbodyConstraints.FreezeAll;
        var things = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var thing in things)
        {
            if(thing.GetComponent<Rigidbody>())
                thing.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
