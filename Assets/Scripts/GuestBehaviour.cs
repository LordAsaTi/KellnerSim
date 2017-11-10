using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuestBehaviour : MonoBehaviour {

    private NavMeshAgent agent;


    void Awake () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	
	void Update () {
		
	}
    public void SetChair(Vector3 destinitionPoint)
    {
        agent.SetDestination(destinitionPoint);
    }
}
