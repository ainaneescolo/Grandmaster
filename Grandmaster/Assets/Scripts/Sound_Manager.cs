using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound_Manager : MonoBehaviour
{
    [SerializeField] private AudioSource effects_Source;
    
    [SerializeField] private Slider _slider;

    void Start()
    {
        
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            StartCoroutine(ChargeSavedMusicVolume());
        }
        
        _slider.onValueChanged.AddListener(val => ChangeMasterVolume(val));
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        effects_Source.PlayOneShot(clip);
    }

    private void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
        
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    
    IEnumerator ChargeSavedMusicVolume()
    {
        yield return new WaitForSeconds(0.3f);
        ChangeMasterVolume(PlayerPrefs.GetFloat("MusicVolume"));
        _slider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
}
