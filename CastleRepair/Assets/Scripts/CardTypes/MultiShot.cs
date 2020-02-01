using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShot : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        myPlayer.GetComponent<PlayerMove>().multiShot = true;
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            myPlayer.GetComponent<PlayerMove>().multiShot = false;
            isActive = false;
        }
    }
}
