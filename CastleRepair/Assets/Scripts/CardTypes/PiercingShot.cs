using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingShot : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        myPlayer.GetComponent<PlayerMove>().pierceShot = true;
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            myPlayer.GetComponent<PlayerMove>().pierceShot = false;
            isActive = false;
        }
    }
}
