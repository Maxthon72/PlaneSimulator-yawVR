using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRotation : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = player.transform.position;
        pos.y += 15;
        this.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y += 10;
        this.transform.position = pos;
    }
}
