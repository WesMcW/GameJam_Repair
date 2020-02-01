using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    ///<summary>The player number, used to set inputs identical to PlayerMove</summary>
    [SerializeField]
    private int playerNum;

    public bool ready = false;

    public int activeCardCount = 0;
    public int maxCardCount = 1;

    public List<GameObject> myHand;

    string a, b, x, y;
    //List<KeyCode> buttons;

    void Start()
    {
        /*
        // temp, for testing on keyboard
        buttons = new List<KeyCode>();
        buttons.Add(KeyCode.A);
        buttons.Add(KeyCode.B);
        buttons.Add(KeyCode.C);
        buttons.Add(KeyCode.D);
        buttons.Add(KeyCode.E);
        */
        a = "A" + playerNum.ToString();
        b = "B" + playerNum.ToString();
        x = "X" + playerNum.ToString();
        y = "Y" + playerNum.ToString();
    }

    void Update()
    {
        if (!ready)
        {
            if (activeCardCount < maxCardCount) // can add more cards
            {
                if (Input.GetButtonDown(a)) useCard(0);
                else if (Input.GetButtonDown(b)) useCard(1);
                else if (Input.GetButtonDown(x)) useCard(2);
                else if (Input.GetButtonDown(y)) useCard(3);
                
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
