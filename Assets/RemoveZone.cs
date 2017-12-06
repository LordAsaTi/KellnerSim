using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveZone : MonoBehaviour {

	private void OnTriggerEnter(Collider coll)
    {
        if(coll.GetComponent<GuestBehaviour>() != null)
        {
            ScoreSystem.Instance.GuestScore(coll.GetComponent<GuestBehaviour>().GetAngerState());
            WaiterGame.Instance.ClearChair(coll.GetComponent<GuestBehaviour>().GetChair());

        }
        Destroy(coll.gameObject);
    }
}
