using SimplePlaneController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DestroyAirplane : MonoBehaviour
{
    public float DestroyLifeTime = 20;
    public int hp = 100;
    public int bulletdamage = 20;

    bool destroyed = false;
    public AirplaneInput input;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Bullet")
        {
            if(!other.gameObject.IsDestroyed())
                Destroy(other.gameObject);
            hp -= bulletdamage;
            if (hp <= 0)
            {
                destroyed = true;
            }
                
            print(hp);
            //this.GetComponent<Animator>().Play("Destruction");
            this.GetComponent<TrailRenderer>().enabled = true;
            this.GetComponent<TrailRenderer>().widthMultiplier = (100-hp)/20;
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
            input.throttle = -1;
            print("destroyed");
            DestroyLifeTime -= Time.deltaTime;
            if (DestroyLifeTime < 0)
            {
              //  Destroy(this.gameObject);
            }
        }
    }
}
