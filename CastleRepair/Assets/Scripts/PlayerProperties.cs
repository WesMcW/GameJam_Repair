using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    List<Card> active_cards;
    // This will track health and be attached to player objects. gameObject refers to what this script is attached to.
    private int unspent_pts;
    private int health = 100;
    public int move_speed = 5; // Arbitrary value for now. Just created a place in memory for it
    bool isDead;
    
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
    }


    int award_points(int pts)
    {   // Awards points to parents 
        unspent_pts += pts;
        return unspent_pts;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
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
}
