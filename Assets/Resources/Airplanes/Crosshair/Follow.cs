using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject follow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // numberOfGuns[i].transform.position = this.transform.position;
        //  numberOfGuns[i].transform.rotation = this.transform.rotation;

        /*bullet.transform.Rotate(transform.rotation.eulerAngles, Space.Self);
        bullet.transform.Rotate(180, 90, 0, Space.Self);
        bullet.transform.position = transform.GetChild(0).transform.position;*/

        transform.Rotate(follow.transform.rotation.eulerAngles, Space.Self);
        this.transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, follow.transform.position.z + 10);
    }
}
