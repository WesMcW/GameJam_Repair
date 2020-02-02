using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    //public AudioClip countdown, gameTheme, deflect, knifeThrow, knifeThrowRock, battleTheme;

    [Header("In this order: 0 = BattleTheme, 1 = Countdown, 2 = Deflection, 3 = KnifeHitRock, 4 = KnifeThrow, 5 = MainTheme")]
    [SerializeField]
    private AudioClip[] allSoundClips;

    private List<AudioSource> allSounds;
    private List<AudioSource> allDeathSounds;

    public enum SoundClip
    {
        Countdown, MainTheme, Deflection, KnifeThrow, KnifeHitRock, BattleTheme, Death, Mute
    };

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
        allSounds = new List<AudioSource> { };
        allDeathSounds = new List<AudioSource> { };

        // Create audio sources of each of the clips
        foreach (AudioClip c in allSoundClips)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            temp.clip = c;
            allSounds.Add(temp);
        }

        foreach (AudioClip deathClip in deathSounds)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            temp.clip = deathClip;
            allDeathSounds.Add(temp);
        }
    }

    public void PlayDeathSound()
    {
        int randomDeath = Random.Range(0, 6);
        allDeathSounds[randomDeath].Play();
    }

    public void PlaySound(SoundClip soundType)
    {
        switch (soundType)
        {
            case SoundClip.BattleTheme:
                allSounds[0].Play();
                break;
            case SoundClip.Countdown:
                allSounds[1].Play();
                break;
            case SoundClip.Deflection:
                allSounds[2].Play();
                break;
            case SoundClip.KnifeHitRock:
                allSounds[3].Play();
                break;
            case SoundClip.KnifeThrow:
                allSounds[4].Play();
                break;
            case SoundClip.MainTheme:
                allSounds[5].Play();
                break;
            case SoundClip.Death:
                PlayDeathSound();
                break;
            case SoundClip.Mute:
                foreach (AudioSource AS in allSounds)
                {
                    AS.Stop();
                }
                break;
        }
    }

    public void SayHello()
    {
        print("Hello");
    }
}
