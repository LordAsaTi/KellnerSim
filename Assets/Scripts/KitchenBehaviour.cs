using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenBehaviour : MonoBehaviour {


    private string[] lines;
    private string[] foodChoices;
    private bool foodReady;
    private bool gotFood;
    private string kitchenName;
    private string orderedFood;

    private void Start()
    {
        lines = new string[]{ "Hallo", "Was bestellt der Kunde?", "Die Küche ist beschäftigt", " ist fertig!" };
        foodChoices = WaiterGame.Instance.GetFoodArray();
        foodReady = true;
        gotFood = true;
        kitchenName = "Küche";
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "ActivePlayer")
        {
            if (foodReady)
            {
                if (gotFood)
                {
                    DialogueSystem.Instance.AddNewDialogue(lines[1], kitchenName);

                    for (int i = 0; i < foodChoices.Length; i++)
                    {
                        string foodname = foodChoices[i];

                        DialogueSystem.Instance.AddChoice(foodname, delegate { StartCoroutine(FoodProcessing(8f, foodname)); DialogueSystem.Instance.CloseDialogue(); });
                    }
                    DialogueSystem.Instance.CreateChoice();
                }
                else
                {
                    DialogueSystem.Instance.AddNewDialogue(orderedFood + lines[3],kitchenName);
                    coll.gameObject.GetComponent<PlayerBehaviour>().SetHeldItem(orderedFood);
                    gotFood = true;
                }

            }
            else
            {
                DialogueSystem.Instance.AddNewDialogue(lines[2], kitchenName);
            }
        }
        
    }

    private IEnumerator FoodProcessing(float waitTime, string foodName)
    {
        foodReady = false;
        gotFood = false;
        yield return new WaitForSeconds(waitTime);
        foodReady = true;
        orderedFood = foodName;
    }
}
