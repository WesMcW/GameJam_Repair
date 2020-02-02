using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainScene : MonoBehaviour
{

   public void ScreenLoader(int SceneIndex)
    {
        PlayerPrefs.SetInt("PlayerCount", GetComponent<PlayerCount>().players);
        SceneManager.LoadScene(SceneIndex);
    }
    public void ScreenLoad(int SceneIndex)
    {
        Destroy(AudioManager.instance.gameObject);
        SceneManager.LoadScene(SceneIndex);
    }
}
