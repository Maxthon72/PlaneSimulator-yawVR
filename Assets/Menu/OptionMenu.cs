using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    public GameObject controllsOption;
    bool controlls;
    TextMeshProUGUI controllsOptionText;
    // Start is called before the first frame update
    void Start()
    {
        controlls = false;
        controllsOptionText = controllsOption.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlls)
        {

            controllsOptionText.text = "controller";
            UnityEngine.Debug.Log(controlls);
        }
        else
        {
            UnityEngine.Debug.Log(controlls);
            controllsOptionText.text = "keyboard";
        }
    }

    public void nextControlls()
    {
        if(controlls)
        {
            controlls = false;
            controllsOptionText.text = "keyboard";
        }
        else
        {
            controlls = true;
            controllsOptionText.text = "controller";
        }
    }

    public void previousControlls()
    {
        if (controlls)
        {
            controlls = false;
            controllsOptionText.text = "keyboard";
        }
        else
        {
            controlls = true;
            controllsOptionText.text = "controller";
        }
    }
}
