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
        if(coll.tag == "ActivePlayer")
        {
            PlayerBehaviour playerBehaviour = coll.GetComponent<PlayerBehaviour>();

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ordering"))
            {
                DialogueSystem.Instance.AddNewDialogue(order + ", bitte.", guestName);
                animator.SetTrigger("Ordered");
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("WaitingForFood"))
            {
                if (playerBehaviour.GetHeldItem() == order)
                {
                    animator.SetTrigger("Ready"); // nochmal überdenken -> hier müsste eigentlich ne coroutin Waiting() starten  also ein state später;
                    DialogueSystem.Instance.AddNewDialogue("Danke", guestName);
                    playerBehaviour.RemoveHeldItem();

                }
                else
                {
                    DialogueSystem.Instance.AddNewDialogue("Ich hatte " + order + " bestellt!", guestName);
                }
            }
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
    public AnimatorStateInfo getState()
    {
        return animator.GetCurrentAnimatorStateInfo(0);
    }
}
