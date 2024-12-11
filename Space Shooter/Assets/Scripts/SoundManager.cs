using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Slider sfxSlider;
    public Slider backgroundSlider;
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

    public void SettingSfxSound()
    {
        float value = sfxSlider.value;
        sfxSound.volume = value;
    }

    public void SettingBackgroundSound()
    {
        float value = backgroundSlider.value;
        backgroundSound.volume = value;
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
