using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;

    private void Start()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        healthbar.health -= damage;
        healthbar.UpdateHealth();
    }
}
