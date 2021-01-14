using UnityEngine;

public class FightSystem : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private ScFPSController scFPS;

    static public bool isFighting;

    private void Start()
    {
        scFPS = FindObjectOfType<ScFPSController>();
    }

    private void Update()
    {
        //Blocking
        if (Input.GetMouseButtonDown(1) && !scFPS.isRunning)
        {
            isFighting = true;
            animator.SetBool("isBlocking", true);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            isFighting = false;
            animator.SetBool("isBlocking", false);
        }

        //Attacking
        if (Input.GetMouseButtonDown(0) && !scFPS.isRunning)
        {
            isFighting = true;
            animator.SetBool("isAttacking", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isFighting = false;
            animator.SetBool("isAttacking", false);
        }
    }
}
