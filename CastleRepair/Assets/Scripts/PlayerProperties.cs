using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperties : MonoBehaviour
{
    List<Card> active_cards;
    // This will track health and be attached to player objects. gameObject refers to what this script is attached to.
    private int unspent_pts;
    private int health = 100;
    //public int move_speed = 5; // Arbitrary value for now. Just created a place in memory for it
    bool isDead;

    public int xp = 0, level = 1;
    int xpToLevel = 2;
    public int points = 0;
    public int score = 0;

    Text pointsTxt, scoreTxt, levelTxt;

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
            gameObject.SetActive(false);
        }

        if (PlayManager.inst.handPhase && pointsTxt != null)
        {
            pointsTxt.text = "Points: " + points;
            pointsTxt.text = "Score: " + score;
            pointsTxt.text = "Level: " + level;
        }
    }


    int award_points(int pts)
    {   // Awards points to parents 
        unspent_pts += pts;
        return unspent_pts;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            Destroy(collision.gameObject);
            health -= 100; // In case we decide at some point we don't want to instantly kill player, here's a mechanism
            if (health <= 0) // by default player will instantly die
            {
                isDead = true;
            }
        }

    } //end collision event

    public void equipCard(Card card)
    {   //call for each card being activated

        active_cards.Add(card);
    }

    public void resetCards()
    {
        foreach (Card a in active_cards)
        {
            a.setCardUnactivate(gameObject);
            active_cards.Remove(a);
        }
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
        }
    }

    public void SetText()
    {
        GameObject tempHand = GetComponent<PlayerHand>().handImg;
        pointsTxt = tempHand.transform.GetChild(2).GetComponent<Text>();
        scoreTxt = tempHand.transform.GetChild(3).GetComponent<Text>();
        levelTxt = tempHand.transform.GetChild(4).GetComponent<Text>();
    }
}
