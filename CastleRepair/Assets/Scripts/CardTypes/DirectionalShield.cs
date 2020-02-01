using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalShield : Card
{
    public GameObject shield;
    GameObject myShield;

    public override void setCardActive(GameObject myPlayer)
    {
        isActive = true;

        int rand = Random.Range(0, 4);
        GameObject temp = Instantiate(shield, myPlayer.transform.position, Quaternion.Euler(new Vector3(0, 0, rand * 90)));
        temp.transform.SetParent(myPlayer.transform);
    }

    public override void setCardUnactivate(GameObject myPlayer)
    {
        Destroy(myShield);

        isActive = false;
    }
}
