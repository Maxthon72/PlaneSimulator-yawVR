using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    public GameObject controllsOption;
    public static int controlls = 0;
    TextMeshProUGUI controllsOptionText;
    // Start is called before the first frame update
    void Start()
    {
        controlls = 0;
        controllsOptionText = controllsOption.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlls==1)
        {

            controllsOptionText.text = "controller";
            UnityEngine.Debug.Log(controlls);
        }
        else if(controlls==0)
        {
            UnityEngine.Debug.Log(controlls);
            controllsOptionText.text = "keyboard";
        }
    }

    public void nextControlls()
    {
        if (controlls == 1)
        {
            controlls = 0;
            controllsOptionText.text = "keyboard";
            PlayerPrefs.SetInt("Controlls", controlls);
        }
        else if (controlls == 0)
        {
            controlls = 1;
            controllsOptionText.text = "controller";
            PlayerPrefs.SetInt("Controlls", controlls);
        }
    }

    public void previousControlls()
    {
        if (controlls == 1)
        {
            controlls = 0;
            controllsOptionText.text = "keyboard";
            PlayerPrefs.SetInt("Controlls", controlls);
        }
        else if (controlls == 0)
        {
            controlls = 1;
            controllsOptionText.text = "controller";
            PlayerPrefs.SetInt("Controlls", controlls);
        }
    }
}
