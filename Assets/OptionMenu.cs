using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionMenuControll : MonoBehaviour
{
    public static bool controller;
    public GameObject controllsOption;
    TextMeshProUGUI controllsOptionText;
    // Start is called before the first frame update
    void Start()
    {
        controllsOptionText = controllsOption.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextControlls()
    {
        if (controller)
        {
            controller = false;
            controllsOptionText.text = "Keybord";
        }
        else
        {
            controller = false;
            controllsOptionText.text = "Controller";
        }
    }

    public void previousControlls()
    {
        if (controller)
        {
            controller = false;
            controllsOptionText.text = "Keybord";
        }
        else
        {
            controller = false;
            controllsOptionText.text = "Controller";
        }
    }
}
