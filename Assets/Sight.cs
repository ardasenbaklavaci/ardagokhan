using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Sight : MonoBehaviour
{
    public enum Sensitivity { Strict, Loose }
    public Sensitivity sensitivity = Sensitivity.Strict;
    public Transform[] wayPoints;
    public Transform player;
    public Transform eyePoint;
    public bool canSeePlayer = false;
    public float sightRange = 45f;
    public Vector3 LastKnowSighting;

    private NavMeshAgent navAgent;
    private int waypointIndex = 0;

    private Transform thisTransform;
    private SphereCollider sphereCollider;
    // Use this for initialization
    void Awake()
    {
        thisTransform = GetComponent<Transform>();
        navAgent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.remainingDistance < 0.5f)
        {
            NextWayPoint();
        }
    }

    void NextWayPoint()
    {
        if (wayPoints.Length == 0)
            return;

        navAgent.destination = wayPoints[waypointIndex].position;
        waypointIndex = (waypointIndex + 1) % wayPoints.Length;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CheckObjectSensitivity();
        }
    }

    void OnTriggerExit()
    {
        canSeePlayer = false;
    }


    bool EnemyFieldOfView()
    {
        Vector3 directionToPlayer = player.position - eyePoint.position;
        float angle = Vector3.Angle(eyePoint.forward, directionToPlayer);

        if (angle <= sightRange)
        {
            return true;
        }
        return false;
    }

    bool ClearView()
    {
        RaycastHit hit;

        if (Physics.Raycast(eyePoint.position, (player.position - eyePoint.position).normalized, out hit, sphereCollider.radius))
        {
            if (hit.transform.CompareTag("Player"))
            {
                LastKnowSighting = player.transform.position;
                return true;
            }
        }
        return false;
    }

    void CheckObjectSensitivity()
    {
        switch (sensitivity)
        {
            case Sensitivity.Strict:
                canSeePlayer = EnemyFieldOfView() && ClearView();
                break;
            case Sensitivity.Loose:
                canSeePlayer = EnemyFieldOfView() || ClearView();
                break;
        }
    }
}