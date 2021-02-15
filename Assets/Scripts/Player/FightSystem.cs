using System.Collections.Generic;
using UnityEngine;


public class FightSystem : MonoBehaviour
{
    static public bool isFighting;

    public GameObject kunai;
    public float resetTime;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange;

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
    private float kunaiTime = 0;
    private float attackTime = 0f;

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
            Block();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            UnBlock();
        }


        // Attacking
        attackTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && combonum < 3 && !scFPS.isRunning)
        {
            Attack();
        }
        reset += Time.deltaTime;
        ResetCombo();

        // Throwing Kunai
        kunaiTime += Time.deltaTime;
        if (Input.GetButton("Throw"))
        {
            if (kunaiTime > cooldown)
            {
                ThrowKunai();
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    private void Block()
    {
        isFighting = true;
        animator.SetBool("isBlocking", true);
    }

    private void UnBlock()
    {
        isFighting = false;
        animator.SetBool("isBlocking", false);
    }

    private void Attack()
    {
        animator.SetTrigger(attackList[combonum]);
        combonum++;
        reset = 0f;

        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        //Damage enemies
        if (animator.GetCurrentAnimatorStateInfo(0).length <= attackTime)
        {

            foreach (Collider enemy in hitEnemies)
            {
                enemy.SendMessage("ApplyDamage", 20f);
            }
            attackTime = 0f;
        }
    }

    // Resets the combo if time is up
    private void ResetCombo()
    {
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
    }

    private void ThrowKunai()
    {
        GameObject newKunai = Instantiate(kunai, spawnSpot.transform.position, spawnSpot.transform.rotation);
        currentKunai = newKunai;
        kunaiTime = 0;
        Destroy(newKunai, 30);
    }
}
