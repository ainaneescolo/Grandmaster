using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Brightness_Manager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private PostProcessProfile brightness;
    [SerializeField] private PostProcessLayer layer;

    private AutoExposure _exposure;

    void Awake()
    {
        if (PlayerPrefs.HasKey("Brightness"))
        {
            StartCoroutine(ChangeSavedBrightness());
        }

        brightness.TryGetSettings(out _exposure);

        _slider.onValueChanged.AddListener(val => ChangeMasterBrightness(val));
    }

    private void ChangeMasterBrightness(float value)
    {
        if (value != 0)
        {
            _exposure.keyValue.value = value;
        }
        else
        {
            _exposure.keyValue.value = .05f;
        }
        
        PlayerPrefs.SetFloat("Brightness", value);
    }
    
    IEnumerator ChangeSavedBrightness()
    {
        yield return new WaitForSeconds(0.3f);
        ChangeMasterBrightness(PlayerPrefs.GetFloat("Brightness"));
        _slider.value = PlayerPrefs.GetFloat("Brightness");
    }
}
