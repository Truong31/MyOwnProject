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
        SoundManager.Instance.musicSound.volume = value;
    }

    public void MusicSetting()
    {
        float value = musicSlider.value;
        SoundManager.Instance.sfxSound.volume = value;
    }

    public void ButtonSound()
    {
        SoundManager.Instance.ClickButton();
    }


}
