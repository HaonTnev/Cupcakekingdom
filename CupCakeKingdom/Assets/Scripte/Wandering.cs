using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;

    public bool seeking;
    public bool fleeing;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    void Update()
    {
        if (seeking)
        {
            Seek(target.transform.position);
        }
        else if (fleeing)
        {
            Flee(target.transform.position);
        }
    }
}
