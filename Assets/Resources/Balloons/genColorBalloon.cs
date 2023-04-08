using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genColorBalloon : MonoBehaviour
{
    public bool randomColor = true;
    public Color color = Color.blue;
    // Start is called before the first frame update
    void Start()
    {
        if(randomColor)
        {
            transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().material.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
