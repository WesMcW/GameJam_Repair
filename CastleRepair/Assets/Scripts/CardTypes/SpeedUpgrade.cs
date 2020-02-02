using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        if(!isActive) myPlayer.GetComponent<PlayerMove>().movementSpeed *= 1.5F;
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        isActive = false;

        myPlayer.GetComponent<PlayerMove>().movementSpeed /= 1.5F;
    }
}