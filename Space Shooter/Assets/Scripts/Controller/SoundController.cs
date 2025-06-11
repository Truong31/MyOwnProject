using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    //Quan ly am luong 
    public Slider sfxSlider;
    public Slider musicSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Music Volumn"))
        {
            VolumnLoading();
        }
        else
        {
            SfxSetting();
            MusicSetting();
        }
    }

    public void SfxSetting()
    {
        float value = sfxSlider.value;
        SoundManager.Instance.audioMixer.SetFloat("Sfx", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Sfx Volumn", value);
    }

    public void MusicSetting()
    {
        float value = musicSlider.value;
        SoundManager.Instance.audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Music Volumn", value);
    }

    private void VolumnLoading()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("Sfx Volumn");
        musicSlider.value = PlayerPrefs.GetFloat("Music Volumn");
        SfxSetting();
        MusicSetting();
    }

    public void ButtonSound()
    {
        SoundManager.Instance.ClickButton();
    }


}
