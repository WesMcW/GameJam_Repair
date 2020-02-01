using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    int playerNum;

    public bool ready = false;

    public int activeCardCount = 0;
    public int maxCardCount = 1;

    public GameObject handImg;
    GameObject cardSpot;

    public List<GameObject> myHand;
    public GameObject[] CardPrefs;
    int[] myDeck;   // [MoveSpeedCard, BarrierCard, ...]

    string a, b, x, y;
    //List<KeyCode> buttons;

    void Start()
    {
        playerNum = GetComponent<PlayerMove>().playerNum;

        a = "A" + playerNum.ToString();
        b = "B" + playerNum.ToString();
        x = "X" + playerNum.ToString();
        y = "Y" + playerNum.ToString();

        // update this as more cards added; this is how many of each card type is in deck
        myDeck = new int[3] {3, 3, 3 };
        addManyCards(3);
    }

    void Update()
    {
        if (!ready)
        {
            if (activeCardCount < maxCardCount) // can add more cards
            {
                if (Input.GetButtonDown(a)) useCard(0);
                else if (Input.GetButtonDown(b)) useCard(1);
                else if (Input.GetButtonDown(x)) useCard(2);
                else if (Input.GetButtonDown(y)) useCard(3);
                
            }
            else if (activeCardCount == maxCardCount)
            {
                Debug.Log(gameObject.name + " is ready");
                ready = true;
                PlayManager.inst.readyPlayers++;
                // changes some ui
            }
        }
    }

    void useCard(int index)
    {
        if (myHand.Count > index)
        {
            myHand[index].GetComponent<Card>().setCardActive(gameObject);
            GetComponent<PlayerProperties>().equipCard(myHand[index].GetComponent<Card>());
            activeCardCount++;
            PlayManager.inst.deleteCards.Add(myHand[index]);
            myHand.RemoveAt(index);
        }
    }

    public void roundReset()
    {
        activeCardCount = 0;
        if (myHand.Count < 4) drawCard();
        ready = false;
    }

    void drawCard()
    {
        int rand = Random.Range(0, myDeck.Length);
        while(myDeck[rand] <= 0) rand = Random.Range(0, myDeck.Length);

        // instantiate new card in the hand, put it in the hand, subtract from the deck
        GameObject newCard = Instantiate(CardPrefs[rand]);
        if (cardSpot == null) cardSpot = handImg.transform.GetChild(0).gameObject;
        else newCard.transform.SetParent(cardSpot.transform);
        myHand.Add(newCard);
        myDeck[rand]--;
    }

    public void addManyCards(int count)
    {
        for(int i = 0; i < count; i++) drawCard();
    }
}
