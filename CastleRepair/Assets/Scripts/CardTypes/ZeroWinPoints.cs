using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroWinPoints : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        PlayManager.inst.noWinPoints = true;
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!myPlayer.GetComponent<PlayerProperties>().isDead && !PlayManager.inst.inGame)
        {
            myPlayer.GetComponent<PlayerProperties>().points += PlayManager.inst.winPoints;
        }
        PlayManager.inst.noWinPoints = false;
        isActive = false;
    }
}
