using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Menu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject exitButton;

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


}
