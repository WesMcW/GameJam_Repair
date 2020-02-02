using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHand : MonoBehaviour
{
    int playerNum;

    public bool ready = false;

    public int activeCardCount = 0;
    public int maxCardCount = 1;

    public GameObject handImg;
    GameObject cardSpot;

    /// <summary>The player properties on this game object</summary>
    private PlayerProperties myProperties;

    public List<GameObject> myHand;
    public GameObject[] CardPrefs;
    int[] myDeck;   // [MoveSpeedCard, BarrierCard, ...]
    int totalCards = 0;

    public Color32 readyColor, menuColor;

    string a, b, x, y, start, select, trigger;
    //List<KeyCode> buttons;

    void Start()
    {
        playerNum = GetComponent<PlayerMove>().playerNum;
        myProperties = GetComponent<PlayerProperties>();

        a = "A" + playerNum.ToString();
        b = "B" + playerNum.ToString();
        x = "X" + playerNum.ToString();
        y = "Y" + playerNum.ToString();
        start = "Start" + playerNum.ToString();
        select = "Select" + playerNum.ToString();
        trigger = "Fire" + playerNum.ToString();

        // update this as more cards added; this is how many of each card type is in deck
        myDeck = new int[14] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7};
        foreach (int i in myDeck) totalCards += i;
        addManyCards(3);
    }

    void Update()
    {
        if (!ready)
        {
            if(Input.GetAxis(trigger) > 0)
            {
                triggerReady();
            }
            if (Input.GetButtonDown(start))
            {
                myProperties.buyScore();
            }
            if (Input.GetButtonDown(select))
            {
                myProperties.buyXP();
            }

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

                handImg.GetComponent<Image>().color = readyColor;
            }
        }

        if (Input.GetAxis(trigger) < 0)
        {
            // show player num
            handImg.GetComponent<Animator>().SetBool("ShowMe", true);
        }
        else
        {
            handImg.GetComponent<Animator>().SetBool("ShowMe", false);
        }
    }

    void useCard(int index)
    {
        if (myHand.Count > index)
        {
            if (!myHand[index].GetComponent<Card>().used)
            {
                //myHand[index].GetComponent<Card>().setCardActive(gameObject);
                myHand[index].GetComponent<Card>().used = true;
                GetComponent<PlayerProperties>().equipCard(myHand[index].GetComponent<Card>());
                activeCardCount++;
                //myHand.RemoveAt(index);
            }
        }
    }

    public void roundReset()
    {
        activeCardCount = 0;
        if (myHand.Count < 4) drawCard();
        if (GetComponent<PlayerProperties>().level > 2 && myHand.Count < 4) drawCard();
        ready = false;
    }

    void triggerReady()
    {
        ready = true;
        PlayManager.inst.readyPlayers++;

        handImg.GetComponent<Image>().color = readyColor;
    }

    void drawCard()
    {
        if (myHand.Count < 4)
        {
            if (totalCards <= 0)
            {
                // resets deck; so we wont run out of cards and be stuck in a loop
                myDeck = new int[14] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };
                foreach (int i in myDeck) totalCards += i;
            }

            int rand = Random.Range(0, myDeck.Length);
            while (myDeck[rand] <= 0) rand = Random.Range(0, myDeck.Length);

            // instantiate new card in the hand, put it in the hand, subtract from the deck
            GameObject newCard = Instantiate(CardPrefs[rand]);
            if (cardSpot == null) cardSpot = handImg.transform.GetChild(0).gameObject;
            newCard.transform.SetParent(cardSpot.transform, false);
            myHand.Add(newCard);
            myDeck[rand]--;
            totalCards--;
        }
    }

    public void addManyCards(int count)
    {
        for(int i = 0; i < count; i++) drawCard();
    }
}
