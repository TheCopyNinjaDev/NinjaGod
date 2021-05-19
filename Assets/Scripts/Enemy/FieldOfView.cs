using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private void Start() 
    {
        StartCoroutine(nameof(FindTargetsWithDelay), .2f);    
    }

    private void Update() 
    {
        //viewRadius = gameObject.GetComponent<EnemyAI>().sightRange;  
    }

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }        
    }

    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        var playerInView = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        foreach (var target in from player in playerInView
            select player.transform 
            into target 
            let dir2Target = (target.position - transform.position).normalized 
            where Vector3.Angle(transform.forward, dir2Target) < viewAngle / 2 
            let dist2Target = Vector3.Distance(target.position, transform.position)
            where !Physics.Raycast(transform.position, dir2Target, dist2Target, obstacleMask)
            select target)
        {
            visibleTargets.Add(target);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
