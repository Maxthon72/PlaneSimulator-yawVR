using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    bool isEndGame;
    public GameObject airplaneObject;
    public EndScreenUI endScreenUI;
    
    // Update is called once per frame
    void Update()
    {
        if (airplaneObject != null)
        {
            DestroyAirplane airplaneScript = airplaneObject.GetComponent<DestroyAirplane>();
            if (airplaneScript != null)
            {
                isEndGame = airplaneScript.displayEndScreen;
                if(isEndGame == true) {
                    print("Airplane destroyed (from end screen manager)");
                    endScreenUI.Setup();
                }
            }
        }
    }
}
