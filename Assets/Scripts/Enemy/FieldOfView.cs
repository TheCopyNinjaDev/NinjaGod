using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine("FindTargetsWithDelay", .2f);    
    }

    private void Update() 
    {
        viewRadius = gameObject.GetComponent<EnemyAI>().sightRange;  
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }        
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] playerInView = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        foreach(Collider player in playerInView)
        {
            Transform target = player.transform;
            Vector3 dir2Target = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dir2Target) < viewAngle / 2)
            {
                float dist2Target = Vector3.Distance(target.position, transform.position);
                if(!Physics.Raycast(transform.position, dir2Target, dist2Target, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
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
