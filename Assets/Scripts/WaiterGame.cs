using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaiterGame : MonoBehaviour {

    public static WaiterGame Instance { get; set; }


    public Transform[] chairArray;
    public Transform[] tableArray;
    public GameObject guestPrefab;
    public GameObject endscreen;
    public Text scoreText;
    public Transform guestSpawnPoint;
    public Transform exitPoint;
    public int guestspawns;
    public float spawnTime;
    public string[] foodArray;
    public string[] guestNames;
    private bool[] freeName;
    private bool[] freeChair;
    private int spawned;

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

        spawned = 0;
        StartCoroutine(SpawnBehaviour(spawnTime));
        
    }
    private IEnumerator SpawnBehaviour(float spawnTime)
    {
        while(spawned < guestspawns)
        {
            yield return new WaitForSeconds(Random.Range(spawnTime, spawnTime+10));
            if(FreeChairCount() > 0)
            {
                SpawnGuest();
                spawned++;
            }
        }

    }
    private void SpawnGuest()
    {
        GameObject guest = Instantiate(guestPrefab, guestSpawnPoint);
        int chairInt = (int)Random.Range(0, chairArray.Length);
        int nameInt = (int)Random.Range(0, guestNames.Length);
        Transform chairPosition = chairArray[CheckFree(chairInt, freeChair)];

        GuestBehaviour guestBehaviour = guest.GetComponent<GuestBehaviour>();

        guestBehaviour.SetChair(chairPosition);
        guestBehaviour.SetTable(GetClosestTable(tableArray, chairPosition.position));

        guestBehaviour.SetOrder(foodArray[Random.Range(0, foodArray.Length)]);

        guestBehaviour.SetName(guestNames[CheckFree(nameInt, freeName)]);
        guestBehaviour.SetExitPoint(exitPoint.position);

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
    public void ClearChair(Transform chair)
    {
        for(int i = 0; i < chairArray.Length; i++)
        {
            if(chair == chairArray[i])
            {
                freeChair[i] = true;
                Debug.Log("IM FREE");
            }
        }
        if (FreeChairCount() == freeChair.Length && spawned == guestspawns)
        {
            GameOver();
        }
    }
    private int FreeChairCount()
    {
        int isFree = 0;
        for (int i = 0; i<freeChair.Length; i++)
        {
            if (freeChair[i])
            {
                isFree++;
            }
        }
        return isFree;
    }
    private void GameOver()
    {
        endscreen.SetActive(true);
        scoreText.text = ScoreSystem.Instance.GetTotalScore().ToString() + " €";
    }
}
