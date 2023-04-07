using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGun : MonoBehaviour
{
    // Start is called before the first frame update
    public float firerate = 2;
    public float velocity = 3f;
    public float mm = 0.1f;
    public float trailSize = 1;
    public float trailLive = 1;
    public int maxBullettArray = 1000;
    public float soundVolume = 100;
    
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
