using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour {
    
    public static ScoreSystem Instance { get; set; }
    private int totalScore;
    private List<GuestBehaviour> guests;
    public int foodPoints;


    private void Awake ()
    {

        totalScore = 0;
        guests = new List<GuestBehaviour>();

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
        totalScore += GetGuestScore(angryState);
    }
    public void AddGuest(GuestBehaviour guestBehaviour)
    {
        guests.Add(guestBehaviour);
    }
    public List<GuestBehaviour> GetGuestList()
    {
        return guests;
    }
    public int GetTotalScore()
    {
        return totalScore;
    }
    public int GetGuestScore(int angryState)
    {
        int score;

        if (angryState >= 3)
        {
            score = -angryState;
        }
        else
        {
            score = foodPoints - angryState;
        }

        return score;
    }

}
