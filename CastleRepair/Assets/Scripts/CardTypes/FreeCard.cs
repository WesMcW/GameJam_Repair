using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCard : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            myPlayer.GetComponent<PlayerHand>().addManyCards(1);

            isActive = false;
        }
    }
}
