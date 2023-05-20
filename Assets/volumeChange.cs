using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class volumeChange : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public TextMeshProUGUI sliderTest;
    float value;
    int valueToShow;
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            // Volume PlayerPref already exists
            value = PlayerPrefs.GetFloat("Volume");
            slider.value = value;

        }
        else
        {
            // Volume PlayerPref does not exist
            value = 0.5f;
            PlayerPrefs.SetFloat("Volume", value);
            slider.value = value;

        }
    }

    // Update is called once per frame
    void Update()
    {
        value = slider.value;

        valueToShow = ((int)(value * 100));
        sliderTest.text = valueToShow.ToString();
        PlayerPrefs.SetFloat("Volume", value);
    }
}
