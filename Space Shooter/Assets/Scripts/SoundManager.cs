using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set;}

    public AudioSource sfxSound;
    public AudioSource backgroundSound;

    public AudioClip click;
    public AudioClip shoot;
    public AudioClip enemyDeath;
    public AudioClip playerDeath;
    public AudioClip beginning;
    public AudioClip playing;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ClickButton()
    {
        sfxSound.clip = click;
        sfxSound.Play();
    }
    public void BeginningSound()
    {
        backgroundSound.clip = beginning;
        backgroundSound.Play();
    }

    public void PlayingSound()
    {
        backgroundSound.clip = playing;
        backgroundSound.Play();
    }
    public void ShootSfx()
    {
        sfxSound.clip = shoot;
        sfxSound.Play();
    }
    public void EnemyDeathSfx()
    {
        sfxSound.clip = enemyDeath;
        sfxSound.Play();
    }
    public void PlayerDeathSfx()
    {
        sfxSound.clip = playerDeath;
        sfxSound.Play();
    }


}
