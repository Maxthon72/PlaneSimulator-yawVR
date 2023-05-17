using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static ReleaseBullet;
using static UnityEngine.ParticleSystem;

public class SetBullet : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Set damage for airplanes")]
    public int bulletDamage = 20;
    [Tooltip("Multipler, change everytime bullet moves")]
    public float speedMultiplier = 0.9999f;
    [Tooltip("Value which determines Y change")]
    public float gravityMul = 0.001f;
    [Tooltip("Velocity of gun bullet, more velocity => faster bullet")]
    public float velocity = 6f;
    [Tooltip("Size of your bullet")]
    public float bulletSize = 1;
    [Tooltip("Size of Fire trail behind bullet")]
    public float trailSizeFire = 1;
    [Tooltip("Size of Smoke trail behind bullet")]
    public float trailSizeSmoke = 1;
    [Tooltip("Time after Fire trail will dissapear")]
    public float trailFirelive = 1;
    [Tooltip("Time after Smoke trail will dissapear")]
    public float trailSmokelive = 1;
    [Tooltip("Time in seconds after bullet will be destroyed")]
    public float bulletLife = 10;

    float bulletLifeLess;

    public Vector3 direction;
    [HideInInspector]
    public SetGun Gun = null;
 //   int sec = 0;
    bool start = true;
    Vector3 initdir = new Vector3(-1, 0, 0);
    void Start()
    {
        if(Gun!=null)
        {
            direction = Gun.gameObject.transform.rotation * initdir;
        }

        bulletLifeLess = bulletLife;

        GetComponent<TrailRenderer>().widthMultiplier = trailSizeFire;
        transform.GetChild(0).GetComponent<TrailRenderer>().widthMultiplier = trailSizeSmoke;
        GetComponent<TrailRenderer>().time = trailFirelive;
        transform.GetChild(0).GetComponent<TrailRenderer>().time = trailSmokelive;

        direction *= velocity;

        //collision
        BoxCollider col = GetComponent<BoxCollider>();
        float zOff = velocity / bulletSize;
        float bulz = col.size.z;
        col.size = new Vector3(col.size.x, col.size.y, zOff);
        col.center = new Vector3(col.center.x, col.center.y, col.center.z - zOff / 2 + bulz/2);
        GetComponent<BoxCollider>().enabled = false;

        transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);

    }

    /*void Init(     
     float velocity,
     float bulletSize,
     float trailSize,
     float trailFirelive,
     float trailSmokelive,
     float bulletLife)
    {

    }*/

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.IsDestroyed())
        {
            if (bulletLifeLess < 0)
                Destroy(gameObject);
            else
            {
                transform.Translate(direction, Space.World);
                bulletLifeLess -= Time.deltaTime;
                if (start)
                {
                    GetComponent<BoxCollider>().enabled = true;
                    start = false;
                }
                direction.x *= speedMultiplier;
                direction.z *= speedMultiplier;

                direction.y -= gravityMul;
            }
        }
    }
}
