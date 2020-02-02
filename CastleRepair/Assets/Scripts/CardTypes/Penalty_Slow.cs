using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty_Slow : Card
{
    int target;
    public override void setCardActive(GameObject myPlayer)
    {
        target = Random.Range(0, PlayerPrefs.GetInt("PlayerCount"));

        myPlayer.GetComponent<PlayerMove>().movementSpeed *= 1.25F;
        PlayManager.inst.Players[target].GetComponent<PlayerMove>().movementSpeed /= 1.25F;

        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            myPlayer.GetComponent<PlayerMove>().movementSpeed /= 1.25F;
            PlayManager.inst.Players[target].GetComponent<PlayerMove>().movementSpeed *= 1.25F;
            isActive = false;
        }
    }
}
