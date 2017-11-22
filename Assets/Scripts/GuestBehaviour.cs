using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuestBehaviour : MonoBehaviour {

    private NavMeshAgent agent;
    private Transform tableTrans;
    private Transform chairTrans;
    private Vector3 exitPoint;
    private Animator animator;
    public GameObject bubble;
    private string guestName;
    private string order;

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
        if(agent.velocity == Vector3.zero && animator.GetCurrentAnimatorStateInfo(0).IsName("Leave"))
        {
            agent.SetDestination(exitPoint);
        }
        bubble.transform.localEulerAngles = new Vector3(0, -transform.localEulerAngles.y, 0);

        

    }
    public void SetChair(Transform destinitionPoint)
    {
        agent.SetDestination(destinitionPoint.position);
        chairTrans = destinitionPoint;
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
                    animator.SetTrigger("GotFood");
                    LookAtTable();
                    DialogueSystem.Instance.AddNewDialogue("Danke", guestName);
                    playerBehaviour.RemoveHeldItem();

                    StartCoroutine(Waiting(Random.Range(5, 15)));

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
    public AnimatorStateInfo getState() //viellicht ein bool draus machen mit IsState(string stateName) mit gleihc isName vergleich?
    {
        return animator.GetCurrentAnimatorStateInfo(0);
    }
    public void GoToNextState(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
    public void SetTable(Transform table)
    {
        tableTrans = table;
    }
    public void SetExitPoint(Vector3 exitPoint)
    {
        this.exitPoint = exitPoint;
    }
    public Transform getChair()
    {
        return chairTrans;
    }
}
