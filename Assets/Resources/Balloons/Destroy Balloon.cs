using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBalloon : MonoBehaviour
{

    public float fallspeed = 1;
    public float DestroyLifeTime = 10;
    public bool destroyed = false;
    static public int balloonNum = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            this.GetComponent<Animator>().Play("Destruction");
            if (!destroyed)
                balloonNum--;
            destroyed = true;
            Destroy(this.GetComponent<Collider>());
            ScoreManager.instance.updateBalloonNum();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        balloonNum++;
        ScoreManager.instance.updateBalloonNum();
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
