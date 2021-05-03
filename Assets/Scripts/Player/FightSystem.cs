using System.Collections.Generic;
using Trowable_things;
using UnityEngine;


public class FightSystem : MonoBehaviour
{
    public static bool IsFighting;

    public static GameObject Kunai;
    public float resetTime;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange;

    private GameObject _spawnSpot;
    public static GameObject CurrentKunai;

    [SerializeField]
    private Animator animator;

    private ScFPSController _scFPS;

    // Combo variables
    private readonly List<string> _attackList = new List<string>(new string[] { "attack1", "attack2", "attack3" });

    private int _combonum;
    private float _reset;
    private const float Cooldown = 1f;
    private float _kunaiTime = 0;
    private static readonly int Reset = Animator.StringToHash("Reset");
    private static readonly int IsBlocking = Animator.StringToHash("isBlocking");
    
    /*Styles zone need rework after adding changing styles*/
    private TrailRenderer fireKatanaTrail;
    /*End of styles zone need rework after adding changing styles*/

    private void Start()
    {
        _scFPS = FindObjectOfType<ScFPSController>();
        _spawnSpot = GameObject.FindGameObjectWithTag("Throwable thing");
        
        /*Styles zone need rework after adding changing styles*/
        fireKatanaTrail = GameObject.Find("FireKatana").GetComponentInChildren<TrailRenderer>();
        /*End of styles zone need rework after adding changing styles*/
    }

    private void Update()
    {
        // Blocking
        if (Input.GetMouseButtonDown(1) && !_scFPS.isRunning)
        {
            Block();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            UnBlock();
        }


        // Attacking
        if (Input.GetButtonDown("Fire1") && _combonum < 3 && !_scFPS.isRunning)
        {
            Attack();
        }
        _reset += Time.deltaTime;
        ResetCombo();

        // Throwing Kunai
        _kunaiTime += Time.deltaTime;
        if (!Input.GetButton("Throw")) return;
        if (_kunaiTime > Cooldown)
        {
            ThrowKunai();
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
        IsFighting = true;
        animator.SetBool(IsBlocking, true);
    }

    private void UnBlock()
    {
        IsFighting = false;
        animator.SetBool(IsBlocking, false);
    }

    private void Attack()
    {
        animator.SetTrigger(_attackList[_combonum]);
        _combonum++;
        _reset = 0f;
        
        /*Styles zone need rework after adding changing styles*/
        fireKatanaTrail.emitting = true;
        Invoke(nameof(EndEmmit), fireKatanaTrail.time);
        /*End of styles zone need rework after adding changing styles*/
    }
    
    /*Styles zone need rework after adding changing styles*/
    private void EndEmmit()
    {
        fireKatanaTrail.emitting = false;
    }
        
    /*End of styles zone need rework after adding changing styles*/

    //Damage enemies
    public void DealDamage()
    {
        // ReSharper disable once Unity.PreferNonAllocApi
        var hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            enemy.SendMessage("ApplyDamage", 20f);
        }
    }

    // Resets the combo if time is up
    private void ResetCombo()
    {
        if (_reset > resetTime)
        {
            animator.SetTrigger(Reset);
            _combonum = 0;
        }
        if (_combonum == 3)
        {
            resetTime = 0;
            _combonum = 0;
        }
        else
        {
            resetTime = animator.GetCurrentAnimatorStateInfo(0).length;
        }
    }

    private void ThrowKunai()
    {
        var selectedKunai = CirculasMenu.SelectedItem;
        var quantity = selectedKunai switch
        {
            0 => KunaiInventory.QuantityUsual,
            1 => KunaiInventory.QuantityTeleport,
            2 => KunaiInventory.QuantityMissile,
            3 => KunaiInventory.QuantityExplosive,
            _ => 0
        };
        if (quantity > 0)
        {
            var newKunai = Instantiate(Kunai, _spawnSpot.transform.position, _spawnSpot.transform.rotation);
            if (newKunai.TryGetComponent(out TeleportKunai kunai))
            {
                CurrentKunai = newKunai;
            }
            _kunaiTime = 0;
            Destroy(newKunai, 30); 
        }
        else
        {
            print("no kunais of this type");
        }
        
    }
}
