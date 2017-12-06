using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveZone : MonoBehaviour {

	private void OnTriggerEnter(Collider coll)
    {
        if(coll.GetComponent<GuestBehaviour>() != null)
        {
            WaiterGame.Instance.ClearChair(coll.GetComponent<GuestBehaviour>().GetChair());
            ScoreSystem.Instance.GuestScore(coll.GetComponent<GuestBehaviour>().GetAngerState());

        }
        Destroy(coll.gameObject);
    }
}
