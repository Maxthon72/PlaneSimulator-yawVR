using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class DisableRotation : MonoBehaviour
{
    public GameObject player;    
    public bool isPlayer;
    public GameObject another;

    // Start is called before the first frame update
    void Start()
    {   
        if(isPlayer)
        {
            Vector3 pos = player.transform.position;
            pos.y += 50;
            this.transform.position = pos;
            this.transform.localEulerAngles = new Vector3(90, player.transform.eulerAngles.y, 0);
        }
        else
        {
            Vector3 pos = another.transform.position;
            pos.y += 50;
            this.transform.position = pos;
            if (player != null)
            {
                this.transform.localEulerAngles = new Vector3(90, player.transform.eulerAngles.y, 0);
            }
            else
            {
                this.transform.localEulerAngles = new Vector3(90, another.transform.eulerAngles.y, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            Vector3 pos = player.transform.position;
            pos.y += 100;
            this.transform.position = pos;
            this.transform.localEulerAngles = new Vector3(90, player.transform.eulerAngles.y, 0);
        }
        else
        {
            Vector3 pos = another.transform.position;
            pos.y += 100;
            this.transform.position = pos;
            if(player!=null)
            {
                this.transform.localEulerAngles = new Vector3(90, player.transform.eulerAngles.y, 0);
                if (another.GetComponent<DestroyBalloon>().destroyed)
                    this.gameObject.SetActive(false);
            }
            else
            {
                this.transform.localEulerAngles = new Vector3(90, another.transform.eulerAngles.y, 0);
                if (another.GetComponent<DestroyAirplane>().destroyed)
                    this.gameObject.SetActive(false);
            }
        }
        

    }
}
