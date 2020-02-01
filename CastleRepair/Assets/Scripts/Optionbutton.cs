using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optionbutton : MonoBehaviour
{
    public GameObject optionMenu;
    

    private void Start()
    {
        optionMenu.SetActive(false);
        
    }

    public void ChangeScreen()
    {
        optionMenu.SetActive(true);
    }
}
