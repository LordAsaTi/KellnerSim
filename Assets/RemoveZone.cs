using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveZone : MonoBehaviour {

	private void OnTriggerEnter(Collider coll)
    {
        WaiterGame.Instance.ClearChair(coll.GetComponent<GuestBehaviour>().getChair());
        Destroy(coll.gameObject);
    }
}
