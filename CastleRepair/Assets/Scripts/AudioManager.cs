using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField]
    private AudioClip[] allSoundClips;

    public AudioSource soundPlayer; 

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
    }

    public void PlayDeathSound()
    {
        int randomDeath = Random.Range(0, 6);
        soundPlayer.PlayOneShot(deathSounds[randomDeath]);
    }
    

    public void SayHello()
    {
        print("Hello");
    }
}
