using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRound : MonoBehaviour
{
    public GameObject cardStage;

    public void cardRound()
    {
        cardStage.SetActive(false);
    }

    public void roundStart()
    {
        PlayManager.inst.StartBattle();
        //gameObject.SetActive(false);
    }

    public void startCountDown()
    {
        //play countdown sound
        AudioManager.instance.PlaySound(AudioManager.SoundClip.Countdown);
    }
}
