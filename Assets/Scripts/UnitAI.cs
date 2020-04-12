using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAI : MonoBehaviour
{

    public float viewAngle;
    [Range(0, 360)]
    public float viewRadius;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();
    public Transform target;
    public Transform way;
    Vector3 destination;
    NavMeshAgent agent;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        StartCoroutine("FindTargetWithDelay", .2f);

    }

    void Update()
    {
        //way = GameObject.FindGameObjectWithTag(currentTarget);
        if (target != null && Vector3.Distance(destination, target.position) > 1.0f)
        {
            destination = target.position;
            agent.destination = destination;
        }
        else
        {
            if (way == null)
            {
                Transform suka;
                if (gameObject.tag == "Dark")
                {
                    suka = GameObject.FindGameObjectWithTag("GGWP").transform;
                } else
                {
                    suka = GameObject.FindGameObjectWithTag("NOWP").transform;
                }
                way = suka;
            }
            destination = way.position;
            agent.destination = destination;
            if (gameObject.transform.position == way.position)
            {
                Transform suka;
                if (gameObject.tag == "Dark")
                {
                    suka = GameObject.FindGameObjectWithTag("NOWP").transform;
                }
                else
                {
                    suka = GameObject.FindGameObjectWithTag("GGWP").transform;
                }
                way = suka;
            }
        }
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
            if (visibleTargets.Count > 0)
            {
                float distance = -1;
                for (int i = 0; i < visibleTargets.Count; i++)
                {
                    if (distance == -1 || distance > Vector3.Distance(transform.position, visibleTargets[i].position))
                    {
                        distance = Vector3.Distance(transform.position, visibleTargets[i].position);
                        target = visibleTargets[i];
                    }
                }
            }
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Debug.Log(targetsInViewRadius[i].tag + " " + this.tag);
            if (targetsInViewRadius[i].tag == this.tag) { continue; }
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }

            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}



