using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        isActive = true;

        myPlayer.GetComponent<PlayerMove>().movementSpeed *= 1.5F;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        isActive = false;

        myPlayer.GetComponent<PlayerMove>().movementSpeed /= 1.5F;
    }
}