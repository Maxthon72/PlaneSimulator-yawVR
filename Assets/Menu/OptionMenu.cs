using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class OptionMenu : MonoBehaviour
{
    public GameObject controllsOption;
    public static int controlls = 0;
    TextMeshProUGUI controllsOptionText;
    // Start is called before the first frame update
    void Start()
    {
        //checking if playerPref controlls already exist
        if (PlayerPrefs.HasKey("Controlls"))
        {
            // HighScore PlayerPref already exists
            controlls = PlayerPrefs.GetInt("Controlls");

        }
        else
        {
            // HighScore PlayerPref does not exist
            controlls = 0;
            PlayerPrefs.SetInt("Controlls", controlls);

        }

        controllsOptionText = controllsOption.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlls==1)
        {

            controllsOptionText.text = "controller";

        }
        else if(controlls==0)
        {

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
            PlayerPrefs.Save();
        }
        else if (controlls == 0)
        {
            controlls = 1;
            controllsOptionText.text = "controller";
            PlayerPrefs.SetInt("Controlls", controlls);
            PlayerPrefs.Save();
        }
    }

    public void previousControlls()
    {
        if (controlls == 1)
        {
            controlls = 0;
            controllsOptionText.text = "keyboard";
            PlayerPrefs.SetInt("Controlls", controlls);
            PlayerPrefs.Save();
        }
        else if (controlls == 0)
        {
            controlls = 1;
            controllsOptionText.text = "controller";
            PlayerPrefs.SetInt("Controlls", controlls);
            PlayerPrefs.Save();
        }
    }
}
