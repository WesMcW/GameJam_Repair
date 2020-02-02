using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Card
{

    // Start is called before the first frame update
    public override void setCardActive(GameObject myPlayer)
    {
        myPlayer.GetComponent<PlayerMove>().canDash = true;
        isActive = true;
    }

    // Update is called once per frame
    public override void setCardUnactivate(GameObject myPlayer)
    {
        myPlayer.GetComponent<PlayerMove>().canDash = false;
        isActive = false;
    }
}
