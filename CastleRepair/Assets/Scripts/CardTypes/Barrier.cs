using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        isActive = true;

        // in player health script; turn on a bool to true, if hit turns bool off for extra life
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        isActive = false;
        // turn off bool in player health script
    }
}
