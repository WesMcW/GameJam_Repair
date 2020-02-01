using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public static PlayManager inst;

    // Used to treat player prefs like a variable
    static string PlayerPrefsPlayerCount = "PlayerCount";

    public bool inGame = false;
    public bool handPhase = false;

    int playerCount;
    int currentMap = -1;
    int winPoints;
    public int readyPlayers = 0;

    public GameObject[] Maps;
    public GameObject[] Players;
    public GameObject CardScreen;
    public GameObject[] PlayerPrefabs;

    public List<GameObject> deleteCards;

    private void Awake()
    {
        if (inst == null) inst = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        deleteCards = new List<GameObject>();
        //PlayerPrefs.SetInt(PlayerPrefsPlayerCount, 2);

        playerCount = PlayerPrefs.GetInt(PlayerPrefsPlayerCount);

        Players = new GameObject[PlayerPrefs.GetInt(PlayerPrefsPlayerCount)];
        for (int i = 0; i < PlayerPrefs.GetInt(PlayerPrefsPlayerCount); i++)
        {
            GameObject temp = Instantiate(PlayerPrefabs[i]);
            Players[i] = temp;
            temp.GetComponent<PlayerHand>().handImg = CardScreen.transform.GetChild(i).gameObject;
            temp.GetComponent<PlayerHand>().handImg.SetActive(true);
        }
        SetMap();
        handPhase = true;
    }

    void Update()
    {
        if (handPhase)
        {
            // check if all players are ready, if all ready start game
            if(readyPlayers == playerCount)
            {
                Debug.Log("All players ready!");

                handPhase = false;
                CardScreen.SetActive(false);
                foreach (GameObject p in Players)
                {
                    p.GetComponent<SpriteRenderer>().enabled = true;
                    p.transform.GetChild(0).gameObject.SetActive(true);
                }

                // start a countdown
                Invoke("StartBattle", 5F);
            }
        }
    }

    public void NewGame()
    {
        foreach (GameObject p in Players)
        {
            p.GetComponent<PlayerHand>().roundReset();
            p.GetComponent<PlayerHand>().handImg.GetComponent<Image>().color = new Color32(0, 0, 0, 93);
        }

        inGame = false;
        handPhase = true;

        CardScreen.SetActive(true);
        SetMap();

        readyPlayers = 0;

        // enable player ui functions (picking cards & spending points)
        // when player pushes [ready button] ready bool = true
        // if all players have ready = true, game starts || in update, constantly checks if all players are reaady
    }

    public void StartBattle()
    {
        Debug.Log("Starting Match!");

        //foreach (GameObject p in Players) p.SetActive(true);
        foreach (GameObject a in deleteCards) Destroy(a);   // destroy all used cards

        foreach (GameObject p in Players)
        {
            p.GetComponent<PlayerMove>().enabled = true;
            p.GetComponent<PlayerHand>().enabled = false;
        }

        // disables ui card controls, enables gameplay controls, maybe has a countdown until game start
    }

    void SetMap()
    {
        Debug.Log("Setting New Map...");

        // pick new map and set spawnpoints
        // ** currently disables all players, can be changed
        if(currentMap != -1) Maps[currentMap].SetActive(false);
        foreach (GameObject p in Players)
        {
            p.GetComponent<SpriteRenderer>().enabled = false;
            p.transform.GetChild(0).gameObject.SetActive(false);
            p.GetComponent<PlayerMove>().enabled = false;
            p.GetComponent<PlayerHand>().enabled = true;
        }

        currentMap = Random.Range(0, 4);
        Maps[currentMap].SetActive(true);

        winPoints = Random.Range(1, 4);
        Debug.Log("Win points this match: " + winPoints);

        // place players in their spawnpoints
        for (int i = 0; i < Players.Length; i++)
        {
            int rand = Random.Range(0, 3);
            while(Maps[currentMap].GetComponent<MapPlayerSpawns>().isUsed[rand]) rand = Random.Range(0, 3);

            Players[i].transform.position = Maps[i].GetComponent<MapPlayerSpawns>().PlayerSpawns[rand];
            Maps[i].GetComponent<MapPlayerSpawns>().isUsed[rand] = true;
        }

        for (int i = 0; i < Players.Length; i++) Maps[currentMap].GetComponent<MapPlayerSpawns>().isUsed[i] = false;
    }
}