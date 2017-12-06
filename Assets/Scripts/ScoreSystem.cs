using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour {
    
    public static ScoreSystem Instance { get; set; }
    private int totalScore;


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
	
}
