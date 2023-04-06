using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    bool v;
    void Update()
    {
      //  if (Input.GetKeyDown(KeyCode.Space)
      //  {
            v = Input.GetKey(KeyCode.Space);
            if (v)
            {
                GetComponent<Animator>().Play("Scene");
            }

            
     //   }
    }
}
