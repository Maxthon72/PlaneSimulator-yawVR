using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AiShoot : MonoBehaviour
{
    [HideInInspector]
    public bool turnOnAi;
    Vector3 initdir = new Vector3(0, 0, 1), vecdest, front;
    GameObject objective;
    [HideInInspector]
    public bool shoot = false;

    // Start is called before the first frame update
    void Start()
    {
        turnOnAi = GetComponent<SetPlane>().turnOnShootAi;
        objective = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(turnOnAi)
        {
            vecdest = objective.transform.position - this.transform.position;
            front = this.transform.rotation * initdir;
            //    print(Mathf.Acos(Vector3.Dot(front, vecdest.normalized)) * 180 / Mathf.PI);
            if (Mathf.Acos(Vector3.Dot(front, vecdest.normalized)) < 0.2) // <12 stopni x/vecdest.magnitude
            {
                shoot = true;
            }
            else shoot = false;
        }
    }
}
