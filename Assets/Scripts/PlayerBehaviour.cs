using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    
    private string heldItem;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void SetHeldItem(string itemName)
    {
        heldItem = itemName;
    }
    public string GetHeldItem()
    {
        return heldItem;
    }
}
