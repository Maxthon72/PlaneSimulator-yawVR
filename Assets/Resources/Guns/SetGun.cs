using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGun : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Rate of fire of your gun (set interval beetween shots, the more the fastest is gun)")]
    public float firerate = 3;
    [Tooltip("Velocity of gun bullet, more velocity => faster bullet")]
    public float velocity = 6f;
    [Tooltip("Size of your bullet (affect also trailSize), bulletsize 8 will simulate caliber 8mm bullet")]
    public float BulletSize = 8f;
    [Tooltip("Size of trail behind bullet")]
    public float trailSize = 1;
    [Tooltip("Time after Fire trail will dissapear")]
    public float fireTrailLive = 0.27f;
    [Tooltip("Time after Smoke trail will dissapear")]
    public float smokeTrailLive = 0.8f;
    [Tooltip("A size of array bullets, after that bullet will replace other bullet")]
    public int maxBullettArray = 100;
    [Tooltip("Time in seconds after bullet will be destroyed")]
    public float bulletLife = 10;
    [Tooltip("Sound volume of a gun shot [0 - 100]")]
    public float soundVolume = 15;
    
    void Start()
    {
        GameObject objPrefab = Resources.Load("FunctionalGun1 Variant") as GameObject;
        GetComponent<Animator>().SetFloat("Speed", firerate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
