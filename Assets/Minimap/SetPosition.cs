using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRotation : MonoBehaviour
{
    public Transform player;

    
    public bool isBallon;
    public Transform plane;
    // Start is called before the first frame update
    void Start()
    {   
        Vector3 pos = player.transform.position;
        pos.y += 50;
        this.transform.position = pos;
        if(isBallon)
            this.transform.localEulerAngles = new Vector3(90, plane.transform.eulerAngles.y, 0);
        else
            this.transform.localEulerAngles = new Vector3(90, player.transform.eulerAngles.y, 0);
    }

    // Update is called once per frame
    void Update()
    {        
        Vector3 pos = player.transform.position;
        pos.y += 50;
        this.transform.position = pos;
        if (isBallon)
            this.transform.localEulerAngles = new Vector3(90, plane.transform.eulerAngles.y, 0);
        else
            this.transform.localEulerAngles = new Vector3(90, player.transform.eulerAngles.y, 0);
    }
}
