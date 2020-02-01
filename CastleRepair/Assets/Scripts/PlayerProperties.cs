using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    // This will track health and be attached to player objects. gameObject refers to what this script is attached to.
    int unspent_pts;
    int health = 100;
    bool isDead;
    List<Card> active_cards; // !!replace GameObject with Card when committted

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
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
            health -= 100; // In case we decide at some point we don't want to instantly kill player, here's a mechanism
            if (health <= 0) // by default player will instantly die
            {
                isDead = true;
            } 
        }
        
    } //end collision event

    public void equipCard( Card card )
    {   //call for each card being activated

        active_cards.Add(card);
    }

    public void resetCards()
    {
        foreach(Card a in active_cards)
        {
            a.setCardUnactivate(gameObject);
            active_cards.Remove(a);
        }
    }
}
