using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    private NavMeshAgent agent;
    private int floorMask;
    private Ray ray;
    private RaycastHit floorHit;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        floorMask = LayerMask.GetMask("Floor");
    }
	
	// Update is called once per frame
	void Update () {
        getInputPosition();
	}
    private void getInputPosition()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
#else
        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

#endif
        if(Physics.Raycast(ray,out floorHit, 100f, floorMask))
        {
            GoToPoint(floorHit.point);
        }
    }
    private void GoToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
