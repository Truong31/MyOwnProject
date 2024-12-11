using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    private Slider volumnSlider;
    public AudioSource sfxSound;
    public AudioSource backgroundSound;

    private void Awake()
    {
        volumnSlider = GetComponent<Slider>();
    }

    public void SettingSfxSound()
    {
        float value = volumnSlider.value;
        sfxSound.volume = value;
    }

    public void SettingBackgroundSound()
    {
        float value = volumnSlider.value;
        backgroundSound.volume = value;
    }
}
