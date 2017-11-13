using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuestBehaviour : MonoBehaviour {

    private NavMeshAgent agent;
    public Transform tableTrans;


    void Awake () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	
	void Update () {
		
        if(agent.velocity == Vector3.zero)
        {
            LookAtTable();
        }
	}
    public void SetChair(Vector3 destinitionPoint)
    {
        agent.SetDestination(destinitionPoint);
    }
    private void LookAtTable()
    {
        transform.LookAt(tableTrans);
    }
}
