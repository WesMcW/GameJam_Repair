using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public Button playBtn;
    public Button optBtn;
    public Button exitBtn;

    private void Start()
    {
        Navigation playNav = new Navigation();
        playNav.mode = Navigation.Mode.Explicit;
        playNav.selectOnDown = optBtn;
        playNav.selectOnUp = exitBtn;
        playBtn.navigation = playNav;

        Navigation optNav = new Navigation();
        optNav.mode = Navigation.Mode.Explicit;
        optNav.selectOnDown = exitBtn;
        optNav.selectOnUp = playBtn;
        optBtn.navigation = optNav;

        Navigation exitNav = new Navigation();
        exitNav.mode = Navigation.Mode.Explicit;
        exitNav.selectOnDown = playBtn;
        exitNav.selectOnUp = optBtn;
        exitBtn.navigation = exitNav;

        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(playBtn.gameObject);
    }

    public void ScreenLoader(int SceneIndex)
    {
        if (SceneIndex != 1) Destroy(AudioManager.instance.gameObject);

        PlayerPrefs.SetInt("PlayerCount", GetComponent<PlayerCount>().players);
        SceneManager.LoadScene(SceneIndex);
    }
    public void ScreenLoad(int SceneIndex)
    {
        if (SceneIndex != 1) Destroy(AudioManager.instance.gameObject);

        SceneManager.LoadScene(SceneIndex);
    }
}
