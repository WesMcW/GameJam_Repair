using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainScene : MonoBehaviour
{

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
