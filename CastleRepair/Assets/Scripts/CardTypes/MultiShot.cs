using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShot : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        myPlayer.GetComponent<PlayerMove>().multiShot = true;
        myPlayer.GetComponent<PlayerMove>().resetCD *= 2F;
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            myPlayer.GetComponent<PlayerMove>().multiShot = false;
            myPlayer.GetComponent<PlayerMove>().resetCD /= 2F;
            isActive = false;
        }
    }
}
