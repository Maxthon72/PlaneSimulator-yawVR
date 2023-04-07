using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.Callbacks;
using UnityEngine;

public class Gun_MoveScript : MonoBehaviour
{
    [Tooltip("Offset of 1st gun position in x axis (from 1 wing to 2 wing)")]
    public float x = 2.73f;
    [Tooltip("Offset of 1st gun position in y axis (under or above wing)")]
    public float y = 0.8835f;
    [Tooltip("Offset of 1st gun position in z axis (before or after wings)")]
    public float z = 0;
    //GameObject boundedPlane;
    [Tooltip("interval size betweeen guns")]
    public float gunOffset = 6;
    [Tooltip("sum of all guns")]
    public GameObject[] numberOfGuns;

    [Tooltip("guns scale in x axis (from 1 wing to 2 wing)")]
    public float scaleX = 10f;
    [Tooltip("guns scale in y axis (under or above wing)")]
    public float scaleY = 10f;
    [Tooltip("guns scale in z axis (before or after wings)")]
    public float scaleZ = 10f;
    ///public float firerate = 2;

    // Start is called before the first frame update
    void Start()
    {
        /*if(numberOfGuns<0 && numberOfGuns>7)
        {
            numberOfGuns = 7;
        }
        if (gunOffset < 0.636) 
        {
            if(gunOffset > 0.814)
            {
                gunOffset = 0.814f;
            }
            else
            {
                gunOffset = 0.636f;
            }
        }*/

       // x += -11.7f * (scale - 0.2f);
       // y += -4.2f * (scale - 0.2f);
        //z

        gunOffset *= 0.1f;

        GameObject objPrefab = Resources.Load("Guns/FunctionalGun1 Variant") as GameObject;
        for (int i = 0; i < numberOfGuns.Length; i++)
        {
            GameObject go = Instantiate(objPrefab) as GameObject;
            go.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
           /// go.GetComponent<Animator>().SetFloat("Speed", firerate);
            numberOfGuns[i] = go;
        }
    }

    // Update is called once per frame
    void Update()
    {

        /*GameObject child = boundedToPlane.transform.GetChild(0).gameObject;
       GameObject child1 = child.transform.GetChild(0).gameObject;
       print(child1.name);*/
        // boundedToPlane.transform.rotation



        /*this.transform.position = boundedToPlane.transform.position;// + new Vector3(x,y,z);
        this.transform.rotation = boundedToPlane.transform.rotation; // = new Vector3(vec.z, vec.y + 90, vec.x);

        copy.transform.position = this.transform.position;
        copy.transform.rotation = this.transform.rotation;

        this.transform.Translate(new Vector3(x, y, z));
        copy.transform.Translate(new Vector3(-x, y, z));

        this.transform.Rotate(new Vector3(0, 90, 0));
        copy.transform.Rotate(new Vector3(0, 90, 0));*/

        // this.transform.localEulerAngles = child1.transform.localEulerAngles + new Vector3(0, 90, 0);
        // this.transform.position = boundedToPlane.transform.position + new Vector3(x, y, z);
        // child.transform.position += new Vector3(1, 1, 1);

        bool leftGun = false;

        for (int i = 0; i < numberOfGuns.Length; i++)
        {
            int j = i / 2;
            numberOfGuns[i].transform.position = this.transform.position;
            numberOfGuns[i].transform.rotation = this.transform.rotation;
            if (leftGun)
            {
                numberOfGuns[i].transform.Translate(new Vector3(-x - gunOffset * j, y, z));
                leftGun = false;
            }
            else
            {
                numberOfGuns[i].transform.Translate(new Vector3(x + gunOffset * j, y, z));
                leftGun = true;
            }
            numberOfGuns[i].transform.Rotate(new Vector3(-90, 90, 0));
        }
    }
}
