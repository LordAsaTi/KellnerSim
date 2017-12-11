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
    private float timeCounter;
    private int angerState;
    private bool angryTrigger;
    public float angerTime;

    private void Awake () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        angryTrigger = false;
        timeCounter = 0f;
        angerState = 0;
    }
	
	
	private void Update () {

        bubble.transform.localEulerAngles = new Vector3(0, -transform.localEulerAngles.y, 0);
		
        if(agent.velocity == Vector3.zero && GetState().IsName("SearchSeat") && transform.localPosition.z != 0)
        {
            LookAtTable();
            animator.SetTrigger("Seated");
            StartCoroutine(Waiting(Random.Range(5, 15)));           //variable Numbers?
            
        }
        if (agent.velocity == Vector3.zero && GetState().IsName("Leave"))
        {
            agent.SetDestination(exitPoint);

            Debug.Log("LeaveState und velocity = 0");
        }
        if (GetState().IsName("Ordering") || GetState().IsName("AngryWaiting") || GetState().IsName("AngryOrdering") || GetState().IsName("WaitingForFood"))
        {
            timeCounter += Time.deltaTime;
        }

        if(timeCounter > angerTime && !angryTrigger)
        {
            angryTrigger = true;
            angerState++;
            animator.SetTrigger("Angry");
        }
        if(timeCounter > (2* angerTime))
        {
            angerState = 3;
            animator.SetTrigger("Ready");
        }
        

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
        timeCounter = 0f;
        angryTrigger = false;
        animator.SetTrigger("Ready");
        Debug.Log("ready");

    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "ActivePlayer")
        {
            PlayerBehaviour playerBehaviour = coll.GetComponent<PlayerBehaviour>();

            if (GetState().IsName("Ordering") || GetState().IsName("AngryOrdering"))
            {
                DialogueSystem.Instance.AddNewDialogue(order + ", bitte.", guestName);
                timeCounter = 0f;
                angryTrigger = false;
                animator.SetTrigger("Ordered");
            }
            else if (GetState().IsName("WaitingForFood") || GetState().IsName("AngryWaiting"))
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
    public AnimatorStateInfo GetState()
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
    public Transform GetChair()
    {
        return chairTrans;
    }
    public int GetAngerState()
    {
        return angerState;
    }
    public string GetGuestName()
    {
        return guestName;
    }
    public string GetOrder()
    {
        return order;
    }
}
