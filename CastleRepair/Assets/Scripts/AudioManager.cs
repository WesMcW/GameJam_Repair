using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField]
    private AudioClip mainTheme, battleTheme;

    public AudioSource soundPlayer;

    private AudioSource musicPlayer;

    public AudioClip[] deathSounds;

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
    

    public void SayHello()
    {
        print("Hello");
    }

    public void ToggleBattle(bool inBattle)
    {
        musicPlayer.Stop();
        if (!inBattle)
        {
            musicPlayer.clip = mainTheme;
            musicPlayer.Play();
        }
        else
        {
            musicPlayer.clip = battleTheme;
            musicPlayer.Play();
        }
    }
}
