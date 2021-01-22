using System.Collections.Generic;
using UnityEngine;


public class FightSystem : MonoBehaviour
{
    static public bool isFighting;

    public GameObject kunai;
    public float resetTime;

    private GameObject spawnSpot;
    private GameObject currentKunai;

    [SerializeField]
    private Animator animator;
    
    private ScFPSController scFPS;

    // Combo variables
    List<string> attackList = new List<string>(new string[] { "attack1", "attack2", "attack3" });

    private int combonum;
    private float reset;
    private float cooldown = 1f;
    private float currentTime = 0;

    private void Start()
    {
        scFPS = FindObjectOfType<ScFPSController>();
        spawnSpot = GameObject.FindGameObjectWithTag("Throwable thing");
        
    }

    private void Update()
    {
        // Blocking
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

        // Attacking
        if(Input.GetButtonDown("Fire1") && combonum < 3)
        {
            animator.SetTrigger(attackList[combonum]);
            combonum++;
            reset = 0f;

        }
        reset += Time.deltaTime;
        if (reset > resetTime)
        {
            animator.SetTrigger("Reset");
            combonum = 0;

        }
        if (combonum == 3)
        {
            resetTime = 0f;
            combonum = 0;
        }
        else
        {
            resetTime = 1f; 
        }

        // Throwing Kunai
        currentTime += Time.deltaTime;
        if (Input.GetButton("Throw"))
        {
            if(currentTime > cooldown)
            {
                GameObject newKunai = Instantiate(kunai, spawnSpot.transform.position, spawnSpot.transform.rotation);
                newKunai.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 5000);
                currentKunai = newKunai;
                currentTime = 0;
                Destroy(newKunai, 30);
            }
        }

        
    }

    private void LateUpdate()
    {
        // Teleport Kunai
        if (Input.GetKey("t") && currentKunai.gameObject.GetComponentInChildren<Kunai>()._readyToTeleport)
        {
            transform.position = new Vector3(currentKunai.transform.position.x, currentKunai.transform.position.y + 2, currentKunai.transform.position.z);
        }
    }
}
