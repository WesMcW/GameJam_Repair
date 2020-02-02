using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedControls : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        // invert player controls
        isActive = true;
        myPlayer.GetComponent<PlayerMove>().inversion = -1;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        // reset player controls
        if (!myPlayer.GetComponent<PlayerProperties>().isDead)
        {
            if (!PlayManager.inst.inGame) myPlayer.GetComponent<PlayerProperties>().points += 3;
        }
        isActive = false;
        myPlayer.GetComponent<PlayerMove>().inversion = 1;
    }
}
