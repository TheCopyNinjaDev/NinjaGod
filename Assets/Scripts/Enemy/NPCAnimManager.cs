using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimManager : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        
    }
}
