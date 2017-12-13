using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenBackground : MonoBehaviour {

    private PlayerMovement playerMove;
    private Vector3 startPosi;
    private GameObject guest;
    private GuestBehaviour guestBehav;
    public Transform kitchen;
    public float waitGuest;

	private void Start ()
    {
        StartCoroutine(LateStart(2));
	}
    private IEnumerator LateStart(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        guest = GameObject.FindGameObjectWithTag("Guest");
        guestBehav = guest.GetComponent<GuestBehaviour>();
        startPosi = playerMove.gameObject.transform.position;
        StartCoroutine(PlayerActions());
    }
    private IEnumerator PlayerActions()
    {
        while (guestBehav.GetState().IsName("SearchSeat")  || guestBehav.GetState().IsName("Waiting"))
        {
            yield return null;
        }
        playerMove.SetStoppingDistance(1f);
        playerMove.GoToPoint(guest.transform.position);
        yield return new WaitForSeconds(1f);
        while (isMoving())
        {
            yield return null;
        }
        guestBehav.GoToNextState("Ordered");
        yield return new WaitForSeconds(waitGuest);
        playerMove.GoToPoint(kitchen.position);
        yield return new WaitForSeconds(1f);
        while (isMoving())
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        playerMove.GoToPoint(guest.transform.position);
        yield return new WaitForSeconds(1f);
        while (isMoving())
        {
            yield return null;
        }
        guestBehav.GoToNextState("Ready");
        yield return new WaitForSeconds(waitGuest);
        playerMove.GoToPoint(startPosi);


    }
    private bool isMoving()
    {
        return playerMove.getVelocity() != Vector3.zero;
    }
}
