using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty_Slow : Card
{
    public override void setCardActive(GameObject myPlayer)
    {
        int target = Random.Range(0, PlayerPrefs.GetInt("PlayerCount"));
        myPlayer.GetComponent<PlayerMove>().pierceShot = true;
        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            myPlayer.GetComponent<PlayerMove>().pierceShot = false;
            isActive = false;
        }
    }
}
