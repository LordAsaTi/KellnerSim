using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterGame : MonoBehaviour {

    public static WaiterGame Instance { get; set; }


    public Transform[] chairArray;
    public Transform[] tableArray;
    public GameObject guestPrefab;
    public Transform guestSpawnPoint;
    public int guestspawns;
    public string[] foodArray;
    public string[] guestNames;
    private bool[] freeName;
    private bool[] freeChair;

    private void Start () {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        freeName = new bool[guestNames.Length];
        for (int i = 0; i < freeName.Length; i++)
        {
            freeName[i] = true;
        }
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
	
    private void SpawnGuest()
    {
        GameObject guest = Instantiate(guestPrefab, guestSpawnPoint);
        int chairInt = (int)Random.Range(0, chairArray.Length);
        int nameInt = (int)Random.Range(0, guestNames.Length);
        Vector3 chairPosition = chairArray[CheckFree(chairInt, freeChair)].position;

        GuestBehaviour guestBehaviour = guest.GetComponent<GuestBehaviour>();

        guestBehaviour.SetChair(chairPosition);
        guestBehaviour.tableTrans = GetClosestTable(tableArray, chairPosition);

        guestBehaviour.SetOrder(foodArray[Random.Range(0, foodArray.Length)]);

        guestBehaviour.SetName(guestNames[CheckFree(nameInt, freeName)]);

    }

    private int CheckFree(int index, bool[] item)
    {
        if (item[index])
        {
            item[index] = false;
            return index;
        }
        else
        {
            if (index == 0)
            {
                return CheckFree(item.Length - 1, item);
            }
            else
            {
                return CheckFree(index - 1, item);
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
    public string[] GetFoodArray()
    {
        return foodArray;
    }

}
