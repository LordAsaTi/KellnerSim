using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHandler : MonoBehaviour {

    private int floorMask;
    private int playerMask;
    private int guestMask;
    private Ray ray;
    private RaycastHit rayHit;
    private GameObject activePlayer;
    private PlayerMovement playerMove;


    // Use this for initialization
    private void Start () {
        floorMask = LayerMask.GetMask("Floor");
        playerMask = LayerMask.GetMask("Player");
        guestMask = LayerMask.GetMask("Guest");
        activePlayer = GameObject.FindGameObjectWithTag("ActivePlayer");
        playerMove = activePlayer.GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	private void Update () {
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
        
        if(Physics.Raycast(ray, out rayHit, 100f, playerMask))
        {
            GameObject hitObject = rayHit.transform.gameObject;
            Debug.Log("Dont Hit me");
            if (hitObject.tag == "Player")
            {
                activePlayer.tag = "Player";
                activePlayer = hitObject;
                playerMove = activePlayer.GetComponent<PlayerMovement>();
                activePlayer.tag = "ActivePlayer";
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


        
    }
}
