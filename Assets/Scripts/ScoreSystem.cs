using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour {
    
    public static ScoreSystem Instance { get; set; }
    private int totalScore;
    public int foodPoints;


    void Awake () {

        totalScore = 0;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void GuestScore(int angryState)
    {
        if(angryState >= 3)
        {
            totalScore += -angryState;
        }
        else
        {
            totalScore += foodPoints - angryState;
        }
    }

}
