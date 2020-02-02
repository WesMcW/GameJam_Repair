using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedControls : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        // invert player controls
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        // reset player controls
        if (!GetComponent<PlayerProperties>().isDead && !PlayManager.inst.inGame) GetComponent<PlayerProperties>().points += 3;
        isActive = false;
    }
}
