using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerProperties : MonoBehaviour
{
    public List<Card> active_cards;
    // This will track health and be attached to player objects. gameObject refers to what this script is attached to.
    private int unspent_pts;
    public int health = 100;
    //public int move_speed = 5; // Arbitrary value for now. Just created a place in memory for it
    public bool isDead;

    public int xp = 0, level = 1;
    int xpToLevel = 2;
    public int points = 0;
    public int score = 0;

    TextMeshProUGUI pointsTxt, scoreTxt, levelTxt;

    public bool ignoreMe;

    // Start is called before the first frame update INITIALIZATION
    void Start()
    {
        active_cards = new List<Card>();
        isDead = false;
        unspent_pts = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true)
        {
            //gameObject.SetActive(false);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (!ignoreMe)
        {
            if (PlayManager.inst.handPhase)
            {
                if (pointsTxt == null) SetText();
                else
                {
                    pointsTxt.text = "Points: " + points;
                    scoreTxt.text = "Score: " + score;
                    levelTxt.text = "Level: " + level;
                }
            }
        }
    }


    int award_points(int pts)
    {   // Awards points to parents 
        unspent_pts += pts;
        return unspent_pts;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            health -= 100; // In case we decide at some point we don't want to instantly kill player, here's a mechanism
            if (health <= 0 && !isDead) // by default player will instantly die
            {
                isDead = true;

                death(collision.GetComponent<Knife>().owner);
            }
            if(!collision.GetComponent<Knife>().pierce) Destroy(collision.gameObject);
        }
    }

    public void equipCard(Card card)
    {   //call for each card being activated

        active_cards.Add(card);
    }

    public void resetCards()
    {
        foreach (Card a in active_cards)
        {
            a.setCardUnactivate(gameObject);
            Destroy(a.gameObject);
        }

        active_cards = new List<Card>();
        isDead = false;
        health = 100;
    }

    public void buyXP()
    {
        if(points > 0)
        {
            xp++;
            if(xp >= xpToLevel)
            {
                xp -= xpToLevel;
                level++;
                xpToLevel *= 2;
                GetComponent<PlayerHand>().maxCardCount++;
                points--;
            }
        }
    }

    public void buyScore()
    {
        if(points > 0)
        {
            score++;
            points--;

            // check for win
            PlayManager.inst.checkForWin(gameObject);
        }
    }

    public void SetText()
    {
        Debug.Log("setting text objects");
        GameObject tempHand = GetComponent<PlayerHand>().handImg;
        pointsTxt = tempHand.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        scoreTxt = tempHand.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        levelTxt = tempHand.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
    }

    public void death(int playerKill)
    {
        // disable player, add to death count, give point to player who killed

        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<PlayerMove>().enabled = false;
        //GetComponent<PlayerHand>().enabled = true;

        GetComponent<Animator>().SetFloat("Hor", 0);
        GetComponent<Animator>().SetFloat("Vert", 0);
        PlayManager.inst.playersDead.Add(gameObject);

        PlayManager.inst.Players[playerKill - 1].GetComponent<PlayerProperties>().points++;
    }
}
