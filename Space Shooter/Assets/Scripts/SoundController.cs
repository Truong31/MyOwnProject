using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    //Quan ly am luong 
    public Slider sfxSlider;
    public Slider musicSlider;
    public void SfxSetting()
    {
        float value = sfxSlider.value;
        SoundManager.Instance.audioMixer.SetFloat("Sfx", Mathf.Log10(value) * 20);
    }

    public void MusicSetting()
    {
        float value = musicSlider.value;
        SoundManager.Instance.audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
    }

    public void ButtonSound()
    {
        SoundManager.Instance.ClickButton();
    }


}
