using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public bool ready = false;

    public int activeCardCount = 0;
    public int maxCardCount = 1;

    public List<GameObject> myHand;
    List<KeyCode> buttons;

    void Start()
    {
        // temp, for testing on keyboard
        buttons = new List<KeyCode>();
        buttons.Add(KeyCode.A);
        buttons.Add(KeyCode.B);
        buttons.Add(KeyCode.C);
        buttons.Add(KeyCode.D);
        buttons.Add(KeyCode.E);
    }

    void Update()
    {
        if (!ready)
        {
            if (activeCardCount < maxCardCount) // can add more cards
            {
                if (Input.GetKeyDown(buttons[0])) useCard(0);
                else if (Input.GetKeyDown(buttons[1])) useCard(1);
                else if (Input.GetKeyDown(buttons[2])) useCard(2);
                else if (Input.GetKeyDown(buttons[3])) useCard(3);
                else if (Input.GetKeyDown(buttons[4])) useCard(4);
            }
            else if (activeCardCount == maxCardCount)
            {
                ready = true;
                // changes some ui
            }
        }
    }

    void useCard(int index)
    {
        if (myHand.Count > index)
        {
            myHand[index].GetComponent<Card>().setCardActive(gameObject);
            GetComponent<PlayerProperties>().equipCard(myHand[index].GetComponent<Card>());
            activeCardCount++;
            PlayManager.inst.deleteCards.Add(myHand[index]);
            myHand.RemoveAt(index);
        }
    }
}
