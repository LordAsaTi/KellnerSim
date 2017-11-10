using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterGame : MonoBehaviour {

    public Transform[] chairArray;
    public GameObject guestPrefab;
    public Transform guestSpawnPoint;

    private bool[] freeChair;

    private void Start () {

        freeChair = new bool[chairArray.Length];
        for (int i = 0; i < freeChair.Length; i++)
        {
            freeChair[i] = true;
        }
        SpawnGuest();

    }
	
	private void Update () {
		
	}
    private void SpawnGuest()
    {
        GameObject guest = Instantiate(guestPrefab, guestSpawnPoint);
        int chairInt = (int)Random.Range(0, chairArray.Length);
        guest.GetComponent<GuestBehaviour>().SetChair(chairArray[CheckChair(chairInt)].position);
    }

    private int CheckChair(int chairInt)
    {
        if (freeChair[chairInt])
        {
            freeChair[chairInt] = false;
            return chairInt;
        }
        else
        {
            if (chairInt == 0)
            {
                return CheckChair(freeChair.Length - 1);
            }
            else
            {
                return CheckChair(chairInt - 1);
            }

        }
    }
}
