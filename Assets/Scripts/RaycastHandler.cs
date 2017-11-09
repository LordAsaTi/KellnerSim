using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHandler : MonoBehaviour {

    private int floorMask;
    private int playerMask;
    private Ray ray;
    private RaycastHit rayHit;
    private GameObject activePlayer;
    // Use this for initialization
    private void Start () {
        floorMask = LayerMask.GetMask("Floor");
        playerMask = LayerMask.GetMask("Player");
        activePlayer = GameObject.FindGameObjectWithTag("ActivePlayer");
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
                activePlayer.tag = "ActivePlayer";
            }
        }
        else if (Physics.Raycast(ray, out rayHit, 100f, floorMask))
        {
            activePlayer.GetComponent<PlayerMovement>().GoToPoint(rayHit.point);
        }


        
    }
}
