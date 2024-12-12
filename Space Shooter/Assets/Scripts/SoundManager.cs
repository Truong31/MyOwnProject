using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set;}

    [Header("Audio Source")]
    public AudioSource sfxSound;
    public AudioSource musicSound;

    [Space]
    [Header("Audio Clip")]
    public AudioClip click;
    public AudioClip shoot;
    public AudioClip enemyDeath;
    public AudioClip playerDeath;
    public AudioClip beginning;
    public AudioClip playing;
    public AudioClip powerUp;
    public AudioClip bomb;
    public AudioClip bossDeath;

    private void Awake()
    {
        if (Instance == null)
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
        musicSound.clip = beginning;
        musicSound.Play();
    }

    public void PlayingSound()
    {
        musicSound.clip = playing;
        musicSound.Play();
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

    public void PowerUpSfx()
    {
        sfxSound.clip = powerUp;
        sfxSound.Play();
    }

    public void BombSfx()
    {
        sfxSound.clip = bomb;
        sfxSound.Play();
    }

    public void BossDeathSfx()
    {
        sfxSound.clip = bossDeath;
        sfxSound.Play();
    }
}
