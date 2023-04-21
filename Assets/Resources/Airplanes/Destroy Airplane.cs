using SimplePlaneController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DestroyAirplane : MonoBehaviour
{
    float DestroyLifeTime;
    int hp;
    int delayExpl;

    [HideInInspector]
    public bool destroyed = false;
    GameObject objPrefab, objPrefab2;
    int massacrated, maxhp;
    bool notMassacre = true;
    AudioSource hittedSound;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Bullet")
        {
            hittedSound.Play();

            if(!other.gameObject.IsDestroyed())
            {
                Destroy(other.gameObject);
                GameObject go = Instantiate(objPrefab) as GameObject;
                go.transform.position = this.transform.position;
            }

            hp -= other.GetComponent<SetBullet>().bulletDamage;
            if (hp <= 0)
            {
                if (this.gameObject.tag == "Player")
                {
                    GetComponent<AirplaneInput>().AI = true;
                    GetComponent<AirplaneInput>().bdown= true;
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
                
            print(hp);
            //this.GetComponent<Animator>().Play("Destruction");
            if(notMassacre)
            {
                int x = (maxhp - hp) / 20;
                this.GetComponent<TrailRenderer>().enabled = true;
                this.GetComponent<TrailRenderer>().widthMultiplier = x;
                this.GetComponent<TrailRenderer>().time = 5 + x/4;
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
        if (this.gameObject.tag == "Player")
            hittedSound = this.transform.GetChild(8).GetComponent<AudioSource>();
        else hittedSound = this.transform.GetChild(7).GetComponent<AudioSource>();
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
                Destroy(this.gameObject);
            }
        }
    }
}
