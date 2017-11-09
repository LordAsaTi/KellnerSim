using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	private void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Hello There");
        if(coll.tag == "Player")
        {
            Debug.Log("General Kenobi");
        }
    }
}
