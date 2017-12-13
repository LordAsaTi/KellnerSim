using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHandler : MonoBehaviour {

    private int floorMask;
    private int playerMask;
    private int guestMask;
    private int kitchenMask;
    private Ray ray;
    private RaycastHit rayHit;
    private GameObject activePlayer;
    private PlayerMovement playerMove;
    private Collider kitchenColl;


    private void Start ()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerMask = LayerMask.GetMask("Player");
        guestMask = LayerMask.GetMask("Guest");
        kitchenMask = LayerMask.GetMask("Kitchen");
        activePlayer = GameObject.FindGameObjectWithTag("ActivePlayer");
        playerMove = activePlayer.GetComponent<PlayerMovement>();
        kitchenColl = GameObject.Find("Kitchen").GetComponent<Collider>();
    }
	
	private void Update ()
    {
        if(!DialogueSystem.Instance.dialogueActive && !WaiterGame.Instance.gameOver)
        {
         GetInputPosition();
        }
	}
    private void GetInputPosition()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        else
        {
            ray.direction = Vector3.up;
        }
#else
        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

#endif
        
        if(Physics.Raycast(ray, out rayHit, 100f, playerMask))
        {
            GameObject hitObject = rayHit.transform.gameObject;
            if (hitObject.tag == "Player")
            {
                activePlayer.tag = "Player";
                activePlayer = hitObject;
                playerMove = activePlayer.GetComponent<PlayerMovement>();
                activePlayer.tag = "ActivePlayer";

                activePlayer.GetComponent<Collider>().enabled = false;
                activePlayer.GetComponent<Collider>().enabled = true;
            }
        }
        else if (Physics.Raycast(ray, out rayHit, 100f, guestMask))
        {
            playerMove.SetStoppingDistance(1f);
            playerMove.GoToPoint(rayHit.transform.position);

        }
        else if (Physics.Raycast(ray, out rayHit, 100f, floorMask))
        {
            playerMove.SetStoppingDistance(0f);
            playerMove.GoToPoint(rayHit.point);
        }
        else if (Physics.Raycast(ray, out rayHit, 100f, kitchenMask))
        {
            kitchenColl.enabled = false;

            kitchenColl.enabled = true;
        }
    }
}
