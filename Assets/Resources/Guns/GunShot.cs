using SimplePlaneController;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using static UnityEditor.Experimental.GraphView.GraphView;
#endif
public class GunShot : MonoBehaviour
{
    bool v;
    [HideInInspector]
    public bool canShoot = true;
    [HideInInspector]
    public bool shooted = false;
    Animator anim;
    AudioSource shotSound;
    float animTime;
    float time;
    AiShoot AI;

    KeyCode keyShoot;

    SetGun scriptSet;
    private void Start()
    {
        keyShoot = this.GetComponentInParent<SetPlane>().shoot;
        AI = transform.parent.GetComponent<AiShoot>();
        scriptSet = this.gameObject.GetComponent<SetGun>();

        anim = GetComponent<Animator>();
        shotSound = this.transform.GetChild(9).GetComponent<AudioSource>();
        //shotSound.volume = scriptSet.soundVolume/100.0f;*/

       // GameObject objPrefab = Resources.Load("Guns/FunctionalGun1 Variant") as GameObject;
        // GameObject go = Instantiate(objPrefab) as GameObject;
        animTime = this.GetComponent<Animator>().runtimeAnimatorController.animationClips.First(a => a.name == "Scene").length / scriptSet.firerate;// / objPrefab.GetComponent<Animator>().GetFloat("Speed");
    }
    void Update()
    {
        if(AI.turnOnAi)
        {
            if (AI.shoot)
            {
                v = true;
            }
            else v = false;
        }
        else
        v = Input.GetKey(keyShoot);
       // v = true;
        if (v && !shooted)
        {
            time = animTime;
            anim.Play("GunShootAnimation");
            // if(!sound.isPlaying)
            shotSound.Play();
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
