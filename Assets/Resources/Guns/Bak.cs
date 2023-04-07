/*using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class ReleaseBullet : MonoBehaviour
{
    float velocity = 0.1f;
    Vector3 direction;
    Vector3 initvalue = new Vector3(-1, 0, 0);
    GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        GameObject objPrefab = Resources.Load("Bullet1") as GameObject;
        bullet = Instantiate(objPrefab) as GameObject;
        bullet.transform.Rotate(this.transform.rotation.eulerAngles, Space.Self);
        bullet.transform.Rotate(180, 90, 0, Space.Self);
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
        bullet.transform.Translate(direction, Space.World);

        //  bullet.transform.rotation = this.transform.rotation;
        //bullet.transform.Rotate(this.transform.rotation.eulerAngles);

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