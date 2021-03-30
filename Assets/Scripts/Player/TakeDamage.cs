using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;

    private void Awake()
    {
        
    }
    private void Update() 
    {
        CheckDeath();
    }

    public void ApplyDamage(float damage)
    {
        healthbar.health -= damage;
        healthbar.UpdateHealth();
    }

    private void CheckDeath()
    {
        if(healthbar.health <= 0)
            GameObject.FindObjectOfType<EnemyAI>().Die();
    }    
}
