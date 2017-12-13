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
    public float timeScale;
    public float foodTime;
    public Transform progressBarBack;
    public Transform progressBarFront;
    private Vector3 progressBarFrontStartPosi;
    private Vector3 progressBarFrontStartScale;


    private void Start()
    {
        lines = new string[]{ "Hallo", "Was bestellt der Kunde?", "Die Küche ist beschäftigt", " ist fertig!" };
        foodChoices = WaiterGame.Instance.GetFoodArray();
        foodReady = true;
        gotFood = true;
        kitchenName = "Küche";

        progressBarFrontStartPosi = progressBarFront.transform.position;
        progressBarFrontStartScale = progressBarFront.transform.localScale;
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

                        DialogueSystem.Instance.AddChoice(foodname, delegate { StartCoroutine(FoodProcessing(foodTime, foodname)); DialogueSystem.Instance.CloseDialogue(); });
                    }
                    DialogueSystem.Instance.CreateChoice();
                }
                else
                {
                    DialogueSystem.Instance.AddNewDialogue(orderedFood + lines[3],kitchenName);
                    coll.gameObject.GetComponent<PlayerBehaviour>().SetHeldItem(orderedFood);
                    gotFood = true;
                    ResetProgress();
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
        float section = (1/ (waitTime*timeScale)) *(progressBarBack.transform.localPosition.x - progressBarFront.transform.localPosition.x);
        for (int i = 0; i < waitTime* timeScale; i++)
        {
            ShowProgress((i + 1) / (waitTime * timeScale), section);
            yield return new WaitForSeconds(1f/timeScale);
        }
        foodReady = true;
        orderedFood = foodName;
        yield return null;
    }
    private void ShowProgress(float progress, float portion)
    {
        progressBarFront.transform.localPosition += new Vector3(portion, 0,0);
        progressBarFront.transform.localScale = new Vector3(progressBarBack.transform.localScale.x * progress, progressBarFront.transform.localScale.y, progressBarFront.transform.localScale.z);
        
    }
    private void ResetProgress()
    {
        progressBarFront.transform.position = progressBarFrontStartPosi;
        progressBarFront.transform.localScale = progressBarFrontStartScale;
    }
}
