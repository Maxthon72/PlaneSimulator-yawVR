using SimplePlaneController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGun : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Rate of fire of your gun (set interval beetween shots, the more the fastest is gun)")]
    public float firerate = 3;
    [Tooltip("A size of array bullets, after that bullet will replace other bullet")]
    public int maxBullettArray = 100;
    [Tooltip("guns scale in x axis (from 1 wing to 2 wing)")]
    public float scaleX = 5;
    [Tooltip("guns scale in y axis (under or above wing)")]
    public float scaleY = 5;
    [Tooltip("guns scale in z axis (before or after wings)")]
    public float scaleZ = 5;
    [Tooltip("Max plane volume")]
    [Range(0.0f, 1.0f)]
    public float maxVolume = 1f;


    [Tooltip("Prefab bullet. seek in assets/sources/bullets")]
    public GameObject bullletPrefab;

    void Start()
    {
        GameObject objPrefab = Resources.Load("FunctionalGun1 Variant") as GameObject;
        GetComponent<Animator>().SetFloat("Speed", firerate);
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        this.transform.GetChild(9).GetComponent<AudioSource>().volume = maxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
