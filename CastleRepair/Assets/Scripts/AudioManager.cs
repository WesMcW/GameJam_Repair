using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField]
    private AudioClip mainTheme, battleTheme, snowTheme;

    public AudioSource soundPlayer;

    private AudioSource musicPlayer;

    public AudioClip[] deathSounds;

    [SerializeField]
    private AudioClip dink;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        soundPlayer = gameObject.AddComponent<AudioSource>();
        musicPlayer = gameObject.AddComponent<AudioSource>();
        ToggleBattle(false);
    }

    public void PlayDeathSound()
    {
        int randomDeath = Random.Range(0, 5);
        soundPlayer.PlayOneShot(deathSounds[randomDeath]);
    }

    public void ToggleBattle(bool inBattle)
    {
        musicPlayer.Stop();
        if (!inBattle)
        {
            musicPlayer.clip = mainTheme;
            musicPlayer.Play();
        }
        else if (PlayManager.inst.currentMap != 3)
        {
            musicPlayer.clip = battleTheme;
            musicPlayer.Play();
        }
        else
        {
            musicPlayer.clip = snowTheme;
            musicPlayer.Play();
        }
    }

    public void PlayDink()
    {
        soundPlayer.PlayOneShot(dink);
    }
}
