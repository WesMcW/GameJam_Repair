using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty_TakePoint : Card
{
    int target;
    public override void setCardActive(GameObject myPlayer)
    {
        target = Random.Range(0, PlayerPrefs.GetInt("PlayerCount"));

        if (PlayManager.inst.Players[target].GetComponent<PlayerProperties>().points > 0)
        {
            myPlayer.GetComponent<PlayerProperties>().points++;
            PlayManager.inst.Players[target].GetComponent<PlayerProperties>().points--;
        }

        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            isActive = false;
        }
    }
}
