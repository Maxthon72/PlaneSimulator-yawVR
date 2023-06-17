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
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject gameModeMenu;
    public GameObject freePlayModeButton;
    public GameObject arcadeModeButton;
    public GameObject nextCotrollsButton;
    public static int controlls = 0;
    TextMeshProUGUI controllsOptionText;

    public GameObject previousControllsButton;
    public GameObject backFromOptionsButton;
    public GameObject volumeSlider;
    public GameObject controllsOption;
    GameObject currentMenu;
    void Start()
    {
        currentMenu = mainMenu;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void openOptionsMenu()
    {
        currentMenu.SetActive(false);
        currentMenu = optionsMenu;
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

    public void openGameModeMenu()
    {
        currentMenu?.SetActive(false);
        currentMenu = gameModeMenu;
        gameModeMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(freePlayModeButton);

    }

    public void openMainMenu()
    {
        currentMenu.SetActive(false);
        currentMenu = mainMenu;
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButton);
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
