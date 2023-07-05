using SimplePlaneController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Arcade : MonoBehaviour
{
    List<GameObject> planes = new List<GameObject>();
    List<GameObject> ballons = new List<GameObject>();
    List<GameObject> minimapIcons = new List<GameObject>();
    GameObject player = null;
    const int minLen = 500;

    GameObject balloon = null;
    GameObject easyPlane = null;
    GameObject normalPlane = null;
    GameObject hardPlane = null;
    GameObject plane = null;

    GameObject parentMiniMap = null;
    GameObject minimapIcon = null;

    int difficult = 1;
    void generateNextObjective()
    { 
        int x = 0;
        bool oneBalloon = true;
        while (difficult > 0) 
        {
            while (true)
            {
                x = (int)Random.Range(1.5f, 3.99f);
                if (x <= difficult) { break; }
            }
                

            print(x);
            switch (x)
            {
                case 1:
                    plane = easyPlane;
                    break;
                case 2:
                    plane = normalPlane;
                    break;
                default:
                    plane = hardPlane;
                    break;
            }

            float numx = (Random.value - 0.5f) * 2 * minLen;
            float numy = 700 + (Random.value - 0.5f) * 300;
            float numz = (Random.value - 0.5f) * 2 * minLen;

            ballons.Add(Instantiate(balloon, player.transform.position + new Vector3(numx, numy - player.transform.position.y, numz), Quaternion.identity));
            minimapIcon.GetComponent<SpriteRenderer>().sprite = Resources.Load("Minimap/balloonIcon", typeof(Sprite)) as Sprite;
            minimapIcon.GetComponent<DisableRotation>().player = player;
            minimapIcon.GetComponent<DisableRotation>().another = ballons[ballons.Count - 1];
            minimapIcons.Add(Instantiate(minimapIcon));
            minimapIcons[minimapIcons.Count-1].transform.parent = parentMiniMap.transform;


            planes.Add(Instantiate(plane, ballons[ballons.Count - 1].transform.position + new Vector3(0, 20, 0), Quaternion.identity));
            minimapIcon.GetComponent<SpriteRenderer>().sprite = Resources.Load("Minimap/planeIcon", typeof(Sprite)) as Sprite;
            minimapIcon.GetComponent<DisableRotation>().player = null;
            minimapIcon.GetComponent<DisableRotation>().another = planes[planes.Count - 1];
            minimapIcons.Add(Instantiate(minimapIcon));
            minimapIcons[minimapIcons.Count - 1].transform.parent = parentMiniMap.transform;

            difficult -= x;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        balloon = Resources.Load("Balloons/AnimatedBalloon Variant") as GameObject;
        easyPlane = Resources.Load("Airplanes/Prefabs/Easy-AirBiplane") as GameObject;
        normalPlane = Resources.Load("Airplanes/Prefabs/Biplane(air)") as GameObject;
        hardPlane = Resources.Load("Airplanes/Prefabs/Hard-AirBiplane") as GameObject;
        player = GameObject.Find("Airplane-SingleProp");

        parentMiniMap = GameObject.Find("MinimapIcons");
        minimapIcon = Resources.Load("Minimap/minimap_Icon") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (DestroyBalloon.balloonNum == 0)
        {
            ScoreManager.instance.AddPoint();
            difficult = ScoreManager.instance.getScore()+1;
            generateNextObjective();
        }
        // print(DestroyBalloon.balloonNum);
    }
}
