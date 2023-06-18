using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenUI : MonoBehaviour
{
    //private EndScreenManager endScreenManager;

    // Start is called before the first frame update
    /*void Start()
    {
        //endScreenManager = FindObjectOfType<EndScreenManager>();
    }*/

    public void Setup()
    {
        gameObject.SetActive(true);
    }
    

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("ArcadeMode");
    }
}
