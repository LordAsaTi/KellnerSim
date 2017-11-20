using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenBackground : MonoBehaviour {

    private PlayerMovement playerMove;
    private PlayerBehaviour playerBehav;
    private GameObject guest;
    public Transform kitchen;

	// Use this for initialization
	private void Start () {
        StartCoroutine(LateStart(2));
	}
    private IEnumerator LateStart(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMovement>();
        playerBehav = player.GetComponent<PlayerBehaviour>();
        guest = GameObject.FindGameObjectWithTag("Guest");
        StartCoroutine(PlayerActions());
    }
    private IEnumerator PlayerActions()
    {
        while (!guest.GetComponent<GuestBehaviour>().getState().IsName("Ordering"))
        {
            yield return null;
        }
        playerMove.SetStoppingDistance(1f);
        playerMove.GoToPoint(guest.transform.position);
        while (playerMove.getVelocity() != Vector3.zero)
        {
            yield return null;
        }
        Debug.Log("fine so far");

        yield return null;

    }
}
