using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playbutton : MonoBehaviour
{
    public GameObject playerCount;
    //public bool playPressed;

    private void Start()
    {
        playerCount.SetActive(false);
        //playPressed = false;
    }

    public void ChangeScreen()
    {
        playerCount.SetActive(true);
    }
   
}
