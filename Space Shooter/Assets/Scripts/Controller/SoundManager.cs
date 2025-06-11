using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set;}

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

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
    public AudioClip winner;
    public AudioClip gameOver;

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

    public void ClickButton()
    {
        sfxSound.PlayOneShot(click);
    }

    public void ShootSfx()
    {
        sfxSound.PlayOneShot(shoot);
    }
    public void EnemyDeathSfx()
    {
        sfxSound.PlayOneShot(enemyDeath);
    }
    public void PlayerDeathSfx()
    {
        sfxSound.PlayOneShot(playerDeath);
    }

    public void PowerUpSfx()
    {
        sfxSound.PlayOneShot(powerUp);
    }

    public void BombSfx()
    {
        sfxSound.PlayOneShot(bomb);
    }

    public void BossDeathSfx()
    {
        sfxSound.PlayOneShot(bossDeath);
    }

    public void GameOverSfx()
    {
        sfxSound.PlayOneShot(gameOver);
    }

    public void WinnerSfx()
    {
        sfxSound.PlayOneShot(winner);
    }

}
