using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBalloon : MonoBehaviour
{

    public float fallspeed = 1;
    public float DestroyLifeTime = 10;
    bool destroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            this.GetComponent<Animator>().Play("Destruction");
            destroyed = true;
            Destroy(this.GetComponent<Collider>());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyed)
        {
            this.transform.Translate(0, -fallspeed, 0);
            DestroyLifeTime -= Time.deltaTime;
            if(DestroyLifeTime<0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
