using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public static audio instance;

    public AudioSource countdown;
    public AudioSource death1;
    public AudioSource death2;
    public AudioSource death3;
    public AudioSource death4;
    public AudioSource death5;
    public AudioSource gameStart;
    public AudioSource deflect;
    public AudioSource knifeThrow;

    private void Awake()
    {
        if (instance == null)
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
}
