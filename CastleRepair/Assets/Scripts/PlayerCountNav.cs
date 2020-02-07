using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCountNav : MonoBehaviour
{
    //Navigation newNav;
    public Slider countSlider;
    public Button playBtn;
    public Button backBtn;

    // Start is called before the first frame update
    void Start()
    {
        Navigation sliderNav = new Navigation();
        sliderNav.mode = Navigation.Mode.Explicit;
        sliderNav.selectOnDown = playBtn;
        sliderNav.selectOnUp = backBtn;
        countSlider.navigation = sliderNav;

        Navigation playNav = new Navigation();
        playNav.mode = Navigation.Mode.Explicit;
        playNav.selectOnDown = backBtn;
        playNav.selectOnUp = countSlider;
        playBtn.navigation = playNav;

        Navigation backNav = new Navigation();
        backNav.mode = Navigation.Mode.Explicit;
        backNav.selectOnDown = countSlider;
        backNav.selectOnUp = playBtn;
        backBtn.navigation = backNav;

        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(playBtn.gameObject);
    }
}
