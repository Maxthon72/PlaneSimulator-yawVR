using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;



public class ReleaseBullet : MonoBehaviour
{
    public class Bullet : MonoBehaviour
    {
        float velocity;
        float mm;
        float trail;
        float tFirelive;
        float tSmokelive;
        GameObject bullet;
        Vector3 direction;
        Vector3 initdir;

        float bulletLife;
        float bulletLifeLess;

        void SetTrail()
        {
            bullet.GetComponent<TrailRenderer>().widthMultiplier = bullet.transform.localScale.y * trail;
            bullet.transform.GetChild(0).GetComponent<TrailRenderer>().widthMultiplier = bullet.transform.localScale.y * trail;

            bullet.GetComponent<TrailRenderer>().time *= tFirelive;
            bullet.transform.GetChild(0).GetComponent<TrailRenderer>().time *= tSmokelive;
        }

        public void Init(float vel, float m, float _trail, float _tFirelive, float _tSmokelive, float _bulletLife, GameObject preFab, Vector3 initd)
        {
            velocity = vel;
            mm = m/2;
            initdir = initd;
            trail = _trail/15;
            tSmokelive = _tSmokelive;
            tFirelive = _tFirelive;
            bulletLife = _bulletLife;

            bulletLifeLess = bulletLife;
            
            bullet = Instantiate(preFab) as GameObject;
            //collision
            BoxCollider col = bullet.GetComponent<BoxCollider>();
            float zOff = -velocity / mm;
            col.size = new Vector3(col.size.x, col.size.y, col.size.z - zOff);
            col.center = new Vector3(col.center.x, col.center.y, col.center.z+zOff/2);

            bullet.transform.localScale = new Vector3(mm, mm, mm);
            SetTrail();

            bullet.transform.Rotate(this.transform.rotation.eulerAngles, Space.Self);
            bullet.transform.Rotate(180, 90, 0, Space.Self);
            // bullet.transform.Rotate(this.transform.rotation.eulerAngles);
            bullet.transform.position = gameObject.transform.GetChild(0).transform.position;
            direction = this.transform.rotation * initdir;
            direction *= velocity;

        }

        public void Set(GameObject preFab)
        {
            bulletLifeLess = bulletLife;
            bullet = Instantiate(preFab) as GameObject;

            //collision
            BoxCollider col = bullet.GetComponent<BoxCollider>();
            float zOff = -velocity / mm;
            col.size = new Vector3(col.size.x, col.size.y, col.size.z - zOff);
            col.center = new Vector3(col.center.x, col.center.y, col.center.z + zOff / 2);

            //bullet.transform.rotation = Quaternion.identity;
            bullet.transform.localScale = new Vector3(mm, mm, mm);
            SetTrail();

            bullet.transform.Rotate(this.transform.rotation.eulerAngles, Space.Self);
            bullet.transform.Rotate(180, 90, 0, Space.Self);
            // bullet.transform.Rotate(this.transform.rotation.eulerAngles);
            bullet.transform.position = gameObject.transform.GetChild(0).transform.position;
            direction = this.transform.rotation * initdir;
            direction *= velocity;
        }

        public void bulletMove()
        {
            if(!bullet.IsDestroyed())
            {
                if (bulletLifeLess < 0)
                    Destroy(bullet.gameObject);
                else
                {
                    bullet.transform.Translate(direction, Space.World);
                    bulletLifeLess -= Time.deltaTime;
                }
            }
        }
    }
    
    public SetGun scriptSet;

    
    Vector3 initvalue = new Vector3(-1, 0, 0);

    public GunShot script;
    List<Bullet> bullets = new List<Bullet>();
    GameObject objPrefab;
    
    //MyBullet.Length; bulletlimit
    int bulletNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        objPrefab = Resources.Load("Bullets/FunctionalBullet1") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (script.canShoot && script.shooted)
        {
            /*bullet = Instantiate(objPrefab) as GameObject;
            bullet.transform.Rotate(this.transform.rotation.eulerAngles, Space.Self);
            bullet.transform.Rotate(180, 90, 0, Space.Self);
            bullet.transform.position = gameObject.transform.GetChild(0).transform.position;*/


            if (bullets.Count < scriptSet.maxBullettArray)
            {
                Bullet NewBullet = gameObject.AddComponent<Bullet>();
                NewBullet.Init(scriptSet.velocity, scriptSet.BulletSize, scriptSet.trailSize, scriptSet.fireTrailLive, scriptSet.smokeTrailLive, scriptSet.bulletLife, objPrefab, initvalue);
                bullets.Add(NewBullet);
            }
            else
            {
                bullets[bulletNum].Set(objPrefab);
                bulletNum++;
                if (bulletNum > bullets.Count-1)
                {
                    bulletNum = 0;
                }
            }

            script.canShoot = false;
            //script.shooted = false;
        }

        //direction = this.transform.rotation * initvalue;
        //   direction = gameObject.transform.GetChild(0).transform.localPosition - gameObject.transform.GetChild(1).transform.localPosition;
        //   direction = direction / direction.magnitude;
        // direction = initvalue*velocity;
        //direction *= velocity;

        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].bulletMove();
        }
    }
}

/*
public class ReleaseBullet : MonoBehaviour
{
    float velocity;
    float mm;
    public SetGun scriptSet;

    Vector3 direction;
    Vector3 initvalue = new Vector3(-1, 0, 0);

    public GunShot script;
    List<GameObject> bullets = new List<GameObject>();
    GameObject objPrefab;
    GameObject bullet;
    //MyBullet.Length; bulletlimit
    int bulletNum = 0;
    public int bulletMax = 1000;

    // Start is called before the first frame update
    void Start()
    {
        objPrefab = Resources.Load("Bullet1") as GameObject;
        velocity = scriptSet.velocity;
        mm = scriptSet.mm;
    }

    // Update is called once per frame
    void Update()
    {
        if (script.canShoot && script.shooted)
        {
            bullet = Instantiate(objPrefab) as GameObject;
            bullet.transform.Rotate(this.transform.rotation.eulerAngles, Space.Self);
            bullet.transform.Rotate(180, 90, 0, Space.Self);
            // bullet.transform.Rotate(this.transform.rotation.eulerAngles);
            bullet.transform.position = gameObject.transform.GetChild(0).transform.position;
            //  bullet.transform.rotation = this.transform.rotation;

            if (bullets.Count < bulletMax)
            {
                bullets.Add(bullet);
            }
            else
            {
                bullets[bulletNum] = bullet;
                bulletNum++;
                if(bulletNum == bulletMax)
                {
                    bulletNum =0;
                }
            }

            bulletNum++;
            script.canShoot = false;
            //script.shooted = false;
        }

        direction = this.transform.rotation * initvalue;
        //   direction = gameObject.transform.GetChild(0).transform.localPosition - gameObject.transform.GetChild(1).transform.localPosition;
        //   direction = direction / direction.magnitude;
        // direction = initvalue*velocity;
        direction *= velocity;

        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].transform.Translate(direction, Space.World);
        }
    }
}*/

/*public class Bullet : MonoBehaviour
{
    float velocity;
    float mm;
    Vector3 direction;
    Vector3 initvalue = new Vector3(-1, 0, 0);

    GameObject bullet;

    public void init(float v,float m, GameObject prefab)
    {
        velocity = v;
        mm = m;

        bullet = Instantiate(prefab) as GameObject;
        bullet.transform.Rotate(this.transform.rotation.eulerAngles, Space.Self);
        bullet.transform.Rotate(180, 90, 0, Space.Self);
        // bullet.transform.Rotate(this.transform.rotation.eulerAngles);
        bullet.transform.position = gameObject.transform.GetChild(0).transform.position;
        //  bullet.transform.rotation = this.transform.rotation;
    }

    public void run()
    {
        direction = this.transform.rotation * initvalue;
        //   direction = gameObject.transform.GetChild(0).transform.localPosition - gameObject.transform.GetChild(1).transform.localPosition;
        //   direction = direction / direction.magnitude;
        // direction = initvalue*velocity;
        direction *= velocity;
        bullet.transform.Translate(direction, Space.World);

        //  bullet.transform.rotation = this.transform.rotation;
        //bullet.transform.Rotate(this.transform.rotation.eulerAngles);
    }
}



public class ReleaseBullet : MonoBehaviour
{
    public float velocity = 0.1f;
    public float mm=1f;

    public GunShot script;
    GameObject objPrefab;
    public Bullet[] MyBullet;

    //MyBullet.Length; bulletlimit
    int bulletNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        objPrefab = Resources.Load("Bullet1") as GameObject;  
    }

    // Update is called once per frame
    void Update()
    {
        if(script.canShoot && script.shooted)
        {
            if (MyBullet.Length < 10000)//TODO: zrobic ograniczenie ze zmiennej MyBullet
                bulletNum = 0;
            MyBullet[bulletNum].init(velocity, mm, objPrefab);
            
            bulletNum++;
            script.canShoot = false;
            script.shooted = false;
        }

        for (int i = 0; i < bulletNum; i++) 
        {
            MyBullet[i].run();
        }
    }
}*/

/*
 *     float velocity = 0.001f;
    Vector3 direction;
    Vector3 initvalue = new Vector3(0, -1, 1);
    GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        GameObject objPrefab = Resources.Load("Bullet1") as GameObject;
        bullet = Instantiate(objPrefab) as GameObject;
        bullet.transform.position = gameObject.transform.GetChild(0).transform.position;
        bullet.transform.rotation = this.transform.rotation;
        bullet.transform.Rotate(new Vector3(0, -90, 0));
    }

    // Update is called once per frame
    void Update()
    {
        direction = this.transform.rotation * initvalue;
        //   direction = gameObject.transform.GetChild(0).transform.localPosition - gameObject.transform.GetChild(1).transform.localPosition;
        //   direction = direction / direction.magnitude;
        // direction = initvalue*velocity;
        print(direction);
        direction *= velocity;
        bullet.transform.Translate(direction);
      //  bullet.transform.rotation = this.transform.rotation;

      
    }*/

/*
     float velocity = 0.001f;
    Vector3 direction;
    Vector3 initvalue = new Vector3(-1, 0, 0);
    GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        GameObject objPrefab = Resources.Load("Bullet1") as GameObject;
        bullet = Instantiate(objPrefab) as GameObject;
       // bullet.transform.Rotate(this.transform.rotation.eulerAngles);
        bullet.transform.position = gameObject.transform.GetChild(0).transform.position;
      //  bullet.transform.rotation = this.transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = this.transform.rotation * initvalue;
        //   direction = gameObject.transform.GetChild(0).transform.localPosition - gameObject.transform.GetChild(1).transform.localPosition;
        //   direction = direction / direction.magnitude;
        // direction = initvalue*velocity;
        print(direction);
        direction *= velocity;
        bullet.transform.Translate(direction);
      //  bullet.transform.rotation = this.transform.rotation;

      
    }

 * */