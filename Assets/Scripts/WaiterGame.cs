using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterGame : MonoBehaviour {

    public Transform[] chairArray;
    public Transform[] tableArray;
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
        SpawnGuest();
        SpawnGuest();
        SpawnGuest();

    }
	
	private void Update () {
		
	}
    private void SpawnGuest()
    {
        GameObject guest = Instantiate(guestPrefab, guestSpawnPoint);
        int chairInt = (int)Random.Range(0, chairArray.Length);
        Vector3 chairPosition = chairArray[CheckChair(chairInt)].position;

        guest.GetComponent<GuestBehaviour>().SetChair(chairPosition);
        guest.GetComponent<GuestBehaviour>().tableTrans = GetClosestTable(tableArray, chairPosition);
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
