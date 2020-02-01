using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePoint : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            myPlayer.GetComponent<PlayerProperties>().points++;

            isActive = false;
        }
    }
}
