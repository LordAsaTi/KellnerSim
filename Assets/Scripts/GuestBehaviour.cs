using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuestBehaviour : MonoBehaviour {

    private NavMeshAgent agent;
    public Transform tableTrans;
    private Animator animator;
    public GameObject bubble;
    private string guestName;
    private string order;
    float counter = 0;

    private void Awake () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
	}
	
	
	private void Update () {
		
        if(agent.velocity == Vector3.zero && animator.GetCurrentAnimatorStateInfo(0).IsName("SearchSeat"))
        {
            LookAtTable();
            animator.SetTrigger("Seated");
            StartCoroutine(Waiting(Random.Range(5, 15)));
            
        }

        bubble.transform.eulerAngles = new Vector3(0, -counter, 0);     //rotation works, but -transform.eulerAngles.y does not
        counter++;
    }
    public void SetChair(Vector3 destinitionPoint)
    {
        agent.SetDestination(destinitionPoint);
    }
    private void LookAtTable()
    {
        transform.LookAt(tableTrans);
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
            DialogueSystem.Instance.AddNewDialogue(order, guestName);
            animator.SetTrigger("Ordered");
        }
    }

    public void SetOrder(string food)
    {
        order = food;
    }
    public void SetName(string name)
    {
        guestName = name;
    }
}
