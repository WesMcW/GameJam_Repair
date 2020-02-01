using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShorterCD : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        myPlayer.GetComponent<PlayerMove>().resetCD *= 0.5F;
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            myPlayer.GetComponent<PlayerMove>().resetCD /= 0.5F;
            isActive = false;
        }
    }
}
