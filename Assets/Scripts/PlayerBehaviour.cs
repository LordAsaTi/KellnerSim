using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    
    private string heldItem;

	
    public void SetHeldItem(string itemName)
    {
        heldItem = itemName;
    }
    public string GetHeldItem()
    {
        return heldItem;
    }
    public void RemoveHeldItem()
    {
        heldItem = null;
    }
}
