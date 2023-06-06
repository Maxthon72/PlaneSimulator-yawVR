using SimplePlaneController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlane : MonoBehaviour
{

    public KeyCode shoot = KeyCode.Space;
    //destroyPlane
    [Tooltip("Time after plane will be released after being in fire mode")]
    public float DestroyLifeTime = 40;
    [Tooltip("HP value")]
    public int hp = 100;
    [Tooltip("Delay beetween each explosion in fire mode and destroyed mode")]
    public int delayExpl = 50;
    [Tooltip("AI will move to player if this set to true, else player can move the plane")]
    public bool AI = false;
    [Tooltip("AI will shoot after this turned ON")]
    public bool turnOnShootAi = false;
    [Tooltip("Max plane volume")]
    [Range(0.0f, 1.0f)]
    public float maxVolume = 1f;
    int controlls;

    // Start is called before the first frame update
    void Start()
    {
        //engine
        if (this.gameObject.tag == "Player")
            maxVolume = PlayerPrefs.GetFloat("Volume") / 2;
        else
            maxVolume = PlayerPrefs.GetFloat("Volume");
        this.transform.GetChild(2).GetComponent<AirplaneEngine>().maxVolume = maxVolume;

        if (PlayerPrefs.HasKey("Controlls"))
        {
            // HighScore PlayerPref already exists
            controlls = PlayerPrefs.GetInt("Controlls");

        }
        else
        {
            // HighScore PlayerPref does not exist
            controlls = 1;
            PlayerPrefs.SetInt("Controlls", controlls);

        }
        if (controlls == 1)
        {
            shoot = KeyCode.JoystickButton0;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
