using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public bool inGame = false;
    bool handPhase = false;

    int currentMap = -1;
    int winPoints;
    public GameObject[] Maps;
    public GameObject[] Players;
    public GameObject CardScreen;
    public GameObject[] PlayerPrefabs;

    void Start()
    {
        Players = new GameObject[PlayerPrefs.GetInt("PlayerCount")];
        for (int i = 0; i < PlayerPrefs.GetInt("PlayerCount"); i++)
        {
            GameObject temp = Instantiate(PlayerPrefabs[i]);
            Players[i] = temp;
        }
        SetMap();
    }

    void Update()
    {
        if (handPhase)
        {
            // check if all players are ready, if all ready start game
        }
    }

    public void NewGame()
    {
        inGame = false;
        handPhase = true;

        CardScreen.SetActive(true);
        SetMap();

        // enable player ui functions (picking cards & spending points)
    }

    public void StartBattle()
    {

    }

    void SetMap()
    {
        if(currentMap != -1) Maps[currentMap].SetActive(false);
        foreach (GameObject p in Players) p.SetActive(false);

        currentMap = Random.Range(0, 4);
        Maps[currentMap].SetActive(true);

        winPoints = Random.Range(1, 4);

        // place players in their spawnpoints
    }
}
