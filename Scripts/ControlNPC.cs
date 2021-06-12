using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlNPC : MonoBehaviour
{
    Animator anim;
    AnimatorStateInfo info;
    Ray ray;
    RaycastHit hit;

//NPC types and speed variable
    public NPC_TYPE myType;
    public enum NPC_TYPE
    {
        SLOW = 0, FAST = 1, VERY_FAST = 2
    }

    [SerializeField]
    [Range(1,10)]
    float speed;
    public GameObject [] waypoints;
    int waypointIndex = 0;

    bool canSee, canHear;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (myType == NPC_TYPE.SLOW) { speed = 3.0f; canSee = true; canHear = false; }
        else if (myType == NPC_TYPE.FAST) { speed = 5.0f; canSee = true; canHear = true; }
        else { speed = 6.0f; canSee = true; canHear = true; }

        GetComponent<NavMeshAgent>().speed = speed;
    }

    // Update is called once per frame
    void Update()
    {

        ray.origin = gameObject.transform.position + Vector3.up;
        ray.direction = gameObject.transform.forward;
        Debug.DrawRay(ray.origin, ray.direction*100, Color.red);

        if (canSee) Look();
        if (canHear) Hear();

        info = anim.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.I))
        {
            anim.SetBool("canSeePlayer", true);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool("canSeePlayer", false);
        }

        GameObject target = GameObject.Find("FPSController");
        float distanceBetweenNPCandTarget = Vector3.Distance(gameObject.transform.position, target.transform.position);

        if (info.IsName("IDLE"))
        {
            //print ("In idle mode");
            GetComponent<NavMeshAgent>().isStopped = true;
        }

        else if (info.IsName("FOLLOW_PLAYER"))
        {
           // print ("In follow mode");
            GetComponent<NavMeshAgent>().destination = GameObject.Find("FPSController").transform.position;
            GetComponent<NavMeshAgent>().isStopped = false;

            if (distanceBetweenNPCandTarget < 3.0f) anim.SetBool("withinAttackRange", true);

        }

        else if (info.IsName("ATTACK_PLAYER"))
        {
             if (distanceBetweenNPCandTarget > 3.0f) anim.SetBool("withinAttackRange", false);
        }

        else if (info.IsName("PATROL"))
        {
            //if (distanceBetweenNPCandTarget > 1.5f) anim.SetBool("withinAttackRange", false);
            GetComponent<NavMeshAgent>().destination = waypoints[waypointIndex].transform.position;
            GetComponent<NavMeshAgent>().isStopped = false;
            float distanceToWayPoint = Vector3.Distance(gameObject.transform.position, waypoints[waypointIndex].transform.position);
            if (distanceToWayPoint < 2.0f) waypointIndex++;
            if (waypointIndex > 4) waypointIndex = 0;
        }

    }

    void Look()
    {
        //print("looking");
        if(Physics.Raycast(ray.origin, ray.direction*100, out hit))
        {
            if (hit.collider.gameObject.tag == "Player") anim.SetBool("canSeePlayer", true); else anim.SetBool("canSeePlayer", false);
            //print(hit.collider.gameObject.name);
        }
    }

    void Hear()
    {
        if (Vector3.Distance(gameObject.transform.position, GameObject.Find("FPSController").transform.position) < 20.0f) 
            anim.SetBool("canHearPlayer", true); else anim.SetBool("canHearPlayer", false);
    }

}
