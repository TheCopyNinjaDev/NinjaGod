using System.Collections.Generic;
using UnityEngine;


public class FightSystem : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private ScFPSController scFPS;

    List<string> attackList = new List<string>(new string[] { "attack1, attack2, attack3" });
    public int combonum;
    public float reset;
    public float resetTime;

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
        if(Input.GetButtonDown("Fire1") && combonum < 3)
        {
            isFighting = true;
            animator.SetTrigger(attackList[combonum]);
            combonum++;
            reset = 0f;
        }
        if(combonum > 0)
        {
            reset += Time.deltaTime;
            if(reset > resetTime)
            {
                animator.SetTrigger("Reset");
                combonum = 0;
                isFighting = false;
            }
        }
        if(combonum == 3)
        {
            resetTime = 3f;
            combonum = 0;
        }
        else
        {
            resetTime = 1f; 
        }
    }
}
