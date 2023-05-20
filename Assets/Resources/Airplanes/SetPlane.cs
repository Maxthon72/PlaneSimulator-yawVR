using SimplePlaneController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlane : MonoBehaviour
{

    public KeyCode shoot;
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

    // Start is called before the first frame update
    void Start()
    {
        //engine
        maxVolume = PlayerPrefs.GetFloat("Volume");
        this.transform.GetChild(2).GetComponent<AirplaneEngine>().maxVolume = maxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
