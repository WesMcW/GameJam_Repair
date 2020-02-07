using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Optionbutton : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject backBtn;
    

    private void Start()
    {
        optionMenu.SetActive(false);
    }

    public void ChangeScreen()
    {
        optionMenu.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(backBtn);

        Navigation btnNav = new Navigation();
        btnNav.selectOnUp = backBtn.GetComponent<Button>();
        btnNav.selectOnDown = backBtn.GetComponent<Button>();
        backBtn.GetComponent<Button>().navigation = btnNav;
    }
}
