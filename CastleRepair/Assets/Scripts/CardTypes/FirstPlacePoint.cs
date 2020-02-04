using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlacePoint : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!myPlayer.GetComponent<PlayerProperties>().isDead)
        {
           if(!PlayManager.inst.inGame) myPlayer.GetComponent<PlayerProperties>().points++;
        }

        isActive = false; 
    }
}
