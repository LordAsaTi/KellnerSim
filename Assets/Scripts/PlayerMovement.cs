using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    private NavMeshAgent agent;
    
    private void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void GoToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void SetStoppingDistance(float distance)
    {
        agent.stoppingDistance = distance;
    }
    public Vector3 getVelocity()
    {
        return agent.velocity;
    }
}
