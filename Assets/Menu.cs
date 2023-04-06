using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
   public void playFreePlayMode()
    {
        SceneManager.LoadScene("ExampleScene");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
