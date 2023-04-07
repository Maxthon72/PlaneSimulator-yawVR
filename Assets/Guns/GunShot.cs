using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GunShot : MonoBehaviour
{
    bool v;
    [HideInInspector]
    public bool canShoot = true;
    [HideInInspector]
    public bool shooted = false;
    Animator anim;
    float animTime;
    float time;

    public SetGun scriptSet;
    private void Start()
    {
        anim = GetComponent<Animator>();

        GameObject objPrefab = Resources.Load("FunctionalGun1 Variant") as GameObject;
        // GameObject go = Instantiate(objPrefab) as GameObject;
        animTime = objPrefab.GetComponent<Animator>().runtimeAnimatorController.animationClips.First(a => a.name == "Scene").length / scriptSet.firerate;// / objPrefab.GetComponent<Animator>().GetFloat("Speed");

    }
    void Update()
    {
        v = Input.GetKey(KeyCode.Space);


        if (v && !shooted)
        {
            time = animTime;
            anim.Play("GunShootAnimation");
            shooted = true;
        }

        if(shooted)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                canShoot = true;
                shooted = false;
            }
        }


    }
}
