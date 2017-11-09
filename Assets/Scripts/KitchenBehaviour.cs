using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenBehaviour : MonoBehaviour {

    private string[] lines = { "hello" };

	private void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Hello There");
        if(coll.tag == "Player")
        {
            Debug.Log("General Kenobi");
        }
        DialogueSystem.Instance.AddNewDialogue(lines, "kitchen");
    }
}
