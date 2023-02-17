using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;

    public float lookAheadMultiplier = 1;
    public float hideDistance = 10;
    public float wanderRadius = 10;
    public float wanderDistance = 20;
    public float wanderJitter = 1;

    public bool seeking;
    public bool fleeing;
    public bool pursue;
    public bool evade;
    public bool wander;
    public bool hide;
    public bool cleverhide;
    public bool hideandseek;

    public Vector3 wanderTarget = Vector3.zero;
    public Vector3 chosenSpot;

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

    void Pursuit()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        if((toTarget > 90 && relativeHeading < 20) || target.GetComponent<CameraMovement>().currentSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<CameraMovement>().currentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead * lookAheadMultiplier);
    }

    void Evasion()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<CameraMovement>().currentSpeed);
        Flee(target.transform.position + target.transform.forward * -lookAhead * lookAheadMultiplier);
    }

    void Wander()
    {
        wanderTarget += new Vector3(Random.Range(-1f, 1f) * wanderJitter, 0, Random.Range(-1f, 1f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    void Hide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for(int i = 0; i < WorldHidePositions.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = WorldHidePositions.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePos = WorldHidePositions.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * hideDistance;

            if(Vector3.Distance(this.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }
        Seek(chosenSpot);
    }

    void CleverHide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 choseDir = Vector3.zero;
        GameObject chosenGO = WorldHidePositions.Instance.GetHidingSpots()[0];

        for (int i = 0; i < WorldHidePositions.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = WorldHidePositions.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePos = WorldHidePositions.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * hideDistance;

            if (Vector3.Distance(this.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                choseDir = hideDir;
                chosenGO = WorldHidePositions.Instance.GetHidingSpots()[i];
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }

        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -choseDir.normalized);
        RaycastHit info;
        float distance = 100f;
        hideCol.Raycast(backRay, out info, distance);

        Seek(info.point + choseDir.normalized * hideDistance);
    }

    bool CanSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;
        if(Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            if(raycastInfo.transform.gameObject.tag == "Player")
            {
                return true;
            }           
        }
        return false;
    }

    bool TargetCanSeeMe()
    {
        Vector3 toAgent = this.transform.position - target.transform.position;
        float lookingAngle = Vector3.Angle(target.transform.forward, toAgent);

        if (lookingAngle < 60) return true;
        return false;
    }

    bool coolDown = false;
    void BehaviourCoolDown()
    {
        coolDown = false;
    }

    bool CloseEnough()
    {

        if (Vector3.Distance(this.transform.position, target.transform.position) < 30) return true;
        return false;
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
        else if (pursue)
        {
            Pursuit();
        }
        else if (evade)
        {
            Evasion();
        }
        else if (wander)
        {
            Wander();
        }
        else if (hide)
        {
            Hide();
        }
        else if (cleverhide)
        {
            if(CanSeeTarget())
            CleverHide();
        }
        else if (hideandseek)
        {
            if (!coolDown)
            {
                if (!CloseEnough())
                {
                    Wander();
                }
                else if (CanSeeTarget() && TargetCanSeeMe())
                {
                        CleverHide();
                        coolDown = true;
                        Invoke("BehaviourCoolDown", 5);
                }
                else Pursuit();
            }
        }
    }
}
