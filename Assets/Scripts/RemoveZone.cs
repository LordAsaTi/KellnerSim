using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveZone : MonoBehaviour {

	private void OnTriggerEnter(Collider coll)
    {
        if(coll.GetComponent<GuestBehaviour>() != null)
        {
            GuestBehaviour guestBehav = coll.GetComponent<GuestBehaviour>();
            ScoreSystem.Instance.GuestScore(guestBehav.GetAngerState());
            ScoreSystem.Instance.AddGuest(guestBehav);
            WaiterGame.Instance.ClearChair(guestBehav.GetChair());

        }
        Destroy(coll.gameObject);
    }
}
