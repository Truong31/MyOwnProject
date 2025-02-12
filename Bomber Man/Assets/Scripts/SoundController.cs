using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [Header("Slider")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            LoadSound();
        }
        else
        {
            SetSfx();
            SetMusic();
        }
    }

    //Dieu chinh am sfx
    public void SetSfx()
    {
        float value = sfxSlider.value;
        SoundManager.instance.audioMixer.SetFloat("Sfx", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Sfx", value);
    }

    //Dieu chinh am nhac nen
    public void SetMusic()
    {
        float value = musicSlider.value;
        SoundManager.instance.audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Music", value);
    }

    //Dieu chinh lai am thanh theo cai dat gan nhat
    private void LoadSound()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("Sfx");
        musicSlider.value = PlayerPrefs.GetFloat("Music");

        SetSfx();
        SetMusic();
    }
}
