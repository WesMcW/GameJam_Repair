using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty_DisableCard : Card
{
    int target;
    public override void setCardActive(GameObject myPlayer)
    {
        target = Random.Range(0, PlayerPrefs.GetInt("PlayerCount"));

        Invoke("disableCard", 0.5F);

        isActive = true;
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        if (!isPermanent)
        {
            isActive = false;
        }
    }

    void disableCard()
    {
        int rand = Random.Range(0, PlayManager.inst.Players[target].GetComponent<PlayerProperties>().active_cards.Count);
        PlayManager.inst.Players[target].GetComponent<PlayerProperties>().active_cards[rand].setCardUnactivate(PlayManager.inst.Players[target]);
    }
}
