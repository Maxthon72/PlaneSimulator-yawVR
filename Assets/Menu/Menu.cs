using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;



public class Menu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject exitButton;
    public GameObject optionsMenu;
    public GameObject levelMenu;
    public GameObject nextCotrollsButton;
    public static int controlls = 0;
    TextMeshProUGUI controllsOptionText;

    public GameObject previousControllsButton;
    public GameObject backFromOptionsButton;
    public GameObject volumeSlider;
    public GameObject controllsOption;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void openOptionsMenu()
    {
        optionsMenu.SetActive(true);
        controllsOptionText = controllsOption.GetComponent<TextMeshProUGUI>();
        //checking if playerPref controlls already exist
        if (PlayerPrefs.HasKey("Controlls"))
        {
            // HighScore PlayerPref already exists
            controlls = PlayerPrefs.GetInt("Controlls");
            if (controlls == 1)
            {

                controllsOptionText.text = "controller";

            }
            else if (controlls == 0)
            {

                controllsOptionText.text = "keyboard";
            }

        }
        else
        {
            // HighScore PlayerPref does not exist
            controlls = 0;
            PlayerPrefs.SetInt("Controlls", controlls);
            controllsOptionText.text = "keyboard";

        }

        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(nextCotrollsButton);
    }

    public void playFreePlayMode()
    { 
        SceneManager.LoadScene("FreePlayMode");
    }

    public void playArcadeMode()
    {     
        SceneManager.LoadScene("ArcadeMode");
    }

    public void exitGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
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
