using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuestBehaviour : MonoBehaviour {

    private NavMeshAgent agent;
    public Transform tableTrans;
    private Animator animator;
    public GameObject bubble;


    private void Awake () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
	}
	
	
	private void Update () {
		
        if(agent.velocity == Vector3.zero && animator.GetCurrentAnimatorStateInfo(0).IsName("SearchSeat"))
        {
            LookAtTable();
            animator.SetTrigger("Seated");
            StartCoroutine(Waiting(3f));
            
        }

    }
    public void SetChair(Vector3 destinitionPoint)
    {
        agent.SetDestination(destinitionPoint);
    }
    private void LookAtTable()
    {
        transform.LookAt(tableTrans);
        //bubble.transform.eulerAngles.Set(Camera.main.transform.rotation.eulerAngles.x, -this.transform.eulerAngles.y, 0);
    }
    private IEnumerator Waiting(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetTrigger("Ready");
        Debug.Log("ready");
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ordering") && coll.tag == "ActivePlayer")
        {
            //Bestellung über DialogueSystem
            Debug.Log("Bestellen bitte!");
        }
    }
}
