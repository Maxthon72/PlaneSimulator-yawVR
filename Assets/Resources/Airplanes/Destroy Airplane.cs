using SimplePlaneController;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;
using UnityEngine.UIElements;

public class DestroyAirplane : MonoBehaviour
{
    float DestroyLifeTime;
    int hp;
    int delayExpl;

    [HideInInspector]
    public bool destroyed = false;
    GameObject objPrefab, objPrefab2;

    ParticleSystem trail;
    float trailLifeTime;
    float trailSize;
    float trailRad;

    int massacrated, maxhp;
    bool notMassacre = true;
    AudioSource hittedSound;

    public bool displayEndScreen = false;
    public HealthBar healthBar;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Bullet")
        {
            hittedSound.Play();

            if(!other.gameObject.IsDestroyed())
            {
                Destroy(other.gameObject);
                GameObject go = Instantiate(objPrefab) as GameObject;
                go.transform.position = other.transform.position;

                go.transform.localScale = new Vector3(1000, 1000, 1000);
               // go.transform.localScale *= other.GetComponent<SetBullet>().bulletSize;
            }

            hp -= other.GetComponent<SetBullet>().bulletDamage;
            if (this.gameObject.tag == "Player" && hp != null)
            {
                healthBar.SetHealth(hp);
            }
            if (hp <= 0)
            {
                if (this.gameObject.tag == "Player")
                {
                   // GetComponent<GamepadControllArcade>().AI = true;
                    GetComponent<GamepadControllArcade>().lost= true;
                }

                destroyed = true;
                if (hp <= massacrated && notMassacre)
                {
                    
                    notMassacre = false;
                    //this.transform.GetChild(0).GetChild(0).transform.Rotate(new Vector3(10f, 5f, 5f));
                    var mats = this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>().materials;
                    foreach(var mat in mats)
                    {
                        mat.color = new Color(0,0,0);
                    }
                    for(int i=1;i< this.transform.GetChild(0).childCount;i++)
                    {
                        Destroy(this.transform.GetChild(0).GetChild(i).gameObject);
                    }
                    foreach (GameObject gun in this.GetComponent<Gun_MoveScript>().numberOfGuns)
                    {
                        Destroy(gun);
                    }
                    this.GetComponent<Gun_MoveScript>().numberOfGuns.Clear();
                    // this.transform.GetChild(0).GetChild(0).transform.Translate(new Vector3(2f, 1f, 0));
                    //go.transform.localScale = new Vector3(1500, 1500, 1500);
                    //Destroy(this.gameObject);
                }
            }
                
            //print(hp);
            //this.GetComponent<Animator>().Play("Destruction");
            if(notMassacre)
            {
                float x = 1 -  (float)hp / (float)maxhp;
                trail.enableEmission = true;
                trail.startLifetime = trailLifeTime * x;
                trail.startSize = trailSize * x;
             //   trail.shape.radius = 
                var newShape = trail.shape;
                newShape.radius = trailRad * x;
                //print(x);
                /*this.GetComponent<TrailRenderer>().enabled = true;
                this.GetComponent<TrailRenderer>().widthMultiplier = x;
                this.GetComponent<TrailRenderer>().time = 5 + x/4;*/
            }
            
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
        var setPlane = this.GetComponent<SetPlane>();
        DestroyLifeTime = setPlane.DestroyLifeTime;
        hp = setPlane.hp;
        delayExpl = setPlane.delayExpl;

        objPrefab = Resources.Load("Explosions/Functional Explosion") as GameObject;
        objPrefab2 = Resources.Load("Explosions/Functional Explosion2") as GameObject;
        massacrated = -hp / 2;
        maxhp = hp;
        if (this.gameObject.tag == "Player" && maxhp != null)
        {
            healthBar.SetMaxHealth(maxhp);
        }

        if (this.gameObject.tag == "Player")
            hittedSound = this.transform.GetChild(8).GetComponent<AudioSource>();
        else hittedSound = this.transform.GetChild(7).GetComponent<AudioSource>();

        if (GetComponent<ParticleSystem>()!=null)
        {
            trail = GetComponent<ParticleSystem>();
            trail.enableEmission = false;
            trailLifeTime = trail.startLifetime;
            trailSize = trail.startSize;
            trailRad = trail.shape.radius;
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
        {
            DestroyLifeTime -= Time.deltaTime;
            delayExpl++;
            if(delayExpl>=25)
            {
                GameObject go = Instantiate(objPrefab2) as GameObject;
                go.transform.position = this.transform.position;
                delayExpl = 0;
            }
            
            
            if (DestroyLifeTime < 0)
            {
                if(this.gameObject.tag=="Player")
                {
                    print("Przegrales");
                    displayEndScreen = true;
                }
                else
                Destroy(this.gameObject);
            }
        }
    }
}
