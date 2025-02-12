using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    public AudioMixer audioMixer;

    [Header("Audio Sound")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio clip")]
    public AudioClip placeBomb;
    public AudioClip bombExplosion;
    public AudioClip playerDeath;
    public AudioClip powerUp;
    public AudioClip click;
    public AudioClip clearStage;
    public AudioClip clearAllStage;
    public AudioClip titleScreen;
    public AudioClip[] backGround;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void BackGround()
    {
        musicSource.clip = backGround[Random.Range(0, backGround.Length)];
        musicSource.Play();
    }

    public void TitleScreen()
    {
        musicSource.clip = titleScreen;
        musicSource.Play();
    }

    public void ClearStage()
    {
        musicSource.clip = clearStage;
        musicSource.Play();
    }

    public void ClearAllStage()
    {
        musicSource.clip = clearAllStage;
        musicSource.Play();
    }

    public void PlayerDeath()
    {
        musicSource.clip = playerDeath;
        musicSource.Play();
    }

    public void PlaceBomb()
    {
        sfxSource.PlayOneShot(placeBomb);
    }

    public void PowerUp()
    {
        sfxSource.PlayOneShot(powerUp);
    }

    public void BombExplosion()
    {
        sfxSource.PlayOneShot(bombExplosion);
    }
}
