using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty_LongerCD : Card
{
    int target;
    public override void setCardActive(GameObject myPlayer)
    {
        target = Random.Range(0, PlayerPrefs.GetInt("PlayerCount"));
        PlayManager.inst.Players[target].GetComponent<PlayerMove>().resetCD /= 0.75F;

        myPlayer.GetComponent<PlayerMove>().resetCD *= 0.75F;

        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            PlayManager.inst.Players[target].GetComponent<PlayerMove>().resetCD *= 0.75F;
            myPlayer.GetComponent<PlayerMove>().resetCD /= 0.75F;

            isActive = false;
        }
    }
}
