using SimplePlaneController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DestroyAirplane : MonoBehaviour
{
    public float DestroyLifeTime = 40;
    public int hp = 100;
    public int bulletdamage = 20;

    bool destroyed = false;
    public AirplaneInput input;
    GameObject objPrefab;
    int delayExpl = 50;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Bullet")
        {
            if(!other.gameObject.IsDestroyed())
            {
                Destroy(other.gameObject);
                GameObject go = Instantiate(objPrefab) as GameObject;
                go.transform.position = this.transform.position;
            }
                
            hp -= bulletdamage;
            if (hp <= 0)
            {
                destroyed = true;
                if (hp <= -bulletdamage - bulletdamage)
                {
                    GameObject go = Instantiate(objPrefab) as GameObject;
                    go.transform.position = this.transform.position;
                    Destroy(this.gameObject);
                }
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
        objPrefab = Resources.Load("Explosions/Functional Explosion") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
        {
            input.throttle = -100;
            DestroyLifeTime -= Time.deltaTime;
            delayExpl++;
            if(delayExpl>=50)
            {
                GameObject go = Instantiate(objPrefab) as GameObject;
                go.transform.position = this.transform.position;
                delayExpl = 0;
            }
            
            
            if (DestroyLifeTime < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
