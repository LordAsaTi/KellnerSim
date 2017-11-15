using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterGame : MonoBehaviour {

    public Transform[] chairArray;
    public Transform[] tableArray;
    public GameObject guestPrefab;
    public Transform guestSpawnPoint;
    public int guestspawns;
    public string[] foodArray;
    public string[] guestNames;

    private bool[] freeChair;

    private void Start () {

        freeChair = new bool[chairArray.Length];
        for (int i = 0; i < freeChair.Length; i++)
        {
            freeChair[i] = true;
        }
        for(int i = 0; i < guestspawns; i++)
        {
            SpawnGuest();
        }
    
    }
	
	private void Update () {
		
	}
    private void SpawnGuest()
    {
        GameObject guest = Instantiate(guestPrefab, guestSpawnPoint);
        int chairInt = (int)Random.Range(0, chairArray.Length);
        Vector3 chairPosition = chairArray[CheckChair(chairInt)].position;

        GuestBehaviour guestBehaviour = guest.GetComponent<GuestBehaviour>();

        guestBehaviour.SetChair(chairPosition);
        guestBehaviour.tableTrans = GetClosestTable(tableArray, chairPosition);

        guestBehaviour.SetOrder(foodArray[Random.Range(0, foodArray.Length)]);
        guestBehaviour.SetName(guestNames[Random.Range(0, guestNames.Length)]);

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
    private Transform GetClosestTable(Transform[] tables, Vector3 posi)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in tables)
        {
            float dist = Vector3.Distance(t.position, posi);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

}
