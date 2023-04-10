using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DestroyAirplane : MonoBehaviour
{
    public float DestroyLifeTime = 10;

    bool destroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Bullet")
        {
            print("BUM?");
            //this.GetComponent<Animator>().Play("Destruction");
            destroyed = true;
            this.GetComponent<TrailRenderer>().enabled = true;
            this.GetComponent<TrailRenderer>().widthMultiplier = 2;
            this.GetComponent<TrailRenderer>().startColor=Color.black;
            transform.GetComponent<Renderer>().material.color = new Color(0.1f, 0.1f, 0.1f);
        }
    }
    //private void OnTriggerEnter(Collider other)
    /*{
        print("BUM?");
        if (other.gameObject.tag == "Bullet")
        {
            //this.GetComponent<Animator>().Play("Destruction");
            destroyed = true;
            this.GetComponent<TrailRenderer>().enabled = true;
        }
    }*/
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
        {
            DestroyLifeTime -= Time.deltaTime;
            if (DestroyLifeTime < 0)
            {
           //     Destroy(this.gameObject);
            }
        }
    }
}
