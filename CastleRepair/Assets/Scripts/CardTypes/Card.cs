using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public bool used = false;
    public bool isActive = false;
    public bool isPermanent = false;

    public string description; // The words on the card
    public string cardName; // Name of the card

    private void Start()
    {
        used = false;
    }

    public virtual void setCardActive(GameObject myPlayer)
    {
        // applies ability to player
        isActive = true;
    }

    public virtual void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            // resets whichever ability this card gave
            isActive = false;
        }
        // if ability does not end on match end
    }
}
