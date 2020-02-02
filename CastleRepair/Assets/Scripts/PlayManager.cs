using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public static PlayManager inst;

    // Used to treat player prefs like a variable
    public static string PlayerPrefsPlayerCount = "PlayerCount";

    public bool inGame = false;
    public bool handPhase = false;

    int playerCount;
    int currentMap = -1;
    int winPoints;
    public int readyPlayers = 0;
    public List<GameObject> playersDead;

    public GameObject[] Maps;
    public GameObject[] Players;
    public GameObject CardScreen;
    public GameObject[] PlayerPrefabs;
    public GameObject winScreen;

    private void Awake()
    {
        if (inst == null) inst = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        playersDead = new List<GameObject>();
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
        else
        {
            if (inGame)
            {
                // check for deaths + 1 == Players.Length, if so invoke new game and reset players -> GetComponent<PlayerHand>().enabled = true;
                if (playersDead.Count + 1 == playerCount)
                {
                    Debug.Log("Game Ended!");
                    inGame = false;

                    foreach (GameObject p in Players)
                    {
                        // finding winner
                        if (!p.GetComponent<PlayerProperties>().isDead)
                        {
                            Debug.Log(p.name + " has won this round!");

                            //p.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            //p.GetComponent<Animator>().SetFloat("Hor", 0);
                            //p.GetComponent<Animator>().SetFloat("Vert", 0);
                            //p.transform.position = Vector3.zero;

                            p.GetComponent<PlayerProperties>().points += winPoints;
                            //break;
                        }
                    }

                    checkForWin(null);
                    Invoke("NewGame", 3F);
                }
                else if(playersDead.Count == playerCount)
                {
                    Debug.Log("Game Ended!");
                    inGame = false;

                    // finding winner
                    Debug.Log(playersDead[playerCount - 1] + " has won this round!");

                    playersDead[playerCount - 1].GetComponent<PlayerProperties>().points += winPoints;

                    //playersDead[playerCount - 1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    //playersDead[playerCount - 1].GetComponent<Animator>().SetFloat("Hor", 0);
                    //playersDead[playerCount - 1].GetComponent<Animator>().SetFloat("Vert", 0);
                    //playersDead[playerCount - 1].transform.position = Vector3.zero;

                    checkForWin(null);
                    Invoke("NewGame", 3F);
                }
            }
        }
    }

    public void NewGame()
    {
        playersDead = new List<GameObject>();

        foreach (GameObject p in Players)
        {
            p.GetComponent<Animator>().SetFloat("Hor", 0);
            p.GetComponent<Animator>().SetFloat("Vert", 0);
            p.GetComponent<PlayerMove>().enabled = false;
            p.GetComponent<PlayerHand>().enabled = true;
            p.GetComponent<PlayerHand>().roundReset();
            p.GetComponent<PlayerProperties>().resetCards();
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
        inGame = true;

        foreach (GameObject p in Players)
        {
            p.GetComponent<PlayerProperties>().activateCards();
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

        currentMap = Random.Range(0, Maps.Length);
        Maps[currentMap].SetActive(true);

        winPoints = Random.Range(1, 4);
        Debug.Log("Win points this match: " + winPoints);

        // place players in their spawnpoints
        for (int i = 0; i < Players.Length; i++)
        {
            int rand = Random.Range(0, 4);
            while(Maps[currentMap].GetComponent<MapPlayerSpawns>().isUsed[rand]) rand = Random.Range(0, 4);

            Players[i].transform.position = Maps[currentMap].GetComponent<MapPlayerSpawns>().PlayerSpawns[rand];
            Maps[currentMap].GetComponent<MapPlayerSpawns>().isUsed[rand] = true;
        }

        for (int i = 0; i < Players.Length; i++) Maps[currentMap].GetComponent<MapPlayerSpawns>().isUsed[i] = false;
    }

    public void checkForWin(GameObject winner)
    {
        if (winner == null)
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (Players[i].GetComponent<PlayerProperties>().score >= 10)
                {
                    CancelInvoke();
                    foreach (GameObject p in Players) p.SetActive(false);
                    winScreen.SetActive(true);
                    winScreen.transform.GetChild(0).GetComponent<Text>().text = "Player " + i + " wins!";
                    break;
                }
            }
        }
        else
        {
            if(winner.GetComponent<PlayerProperties>().score >= 10)
            {
                CancelInvoke();
                int winID = -1;
                for (int i = 0; i < playerCount; i++)
                {
                    if (Players[i] == winner) winID = i;
                    Players[i].SetActive(false);
                }
                winScreen.SetActive(true);
                winScreen.transform.GetChild(0).GetComponent<Text>().text = "Player " + winID + " wins!";
            }
        }
    }
}