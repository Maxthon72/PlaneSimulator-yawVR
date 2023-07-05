using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text scoreText;
    public TMP_Text planesNum;
    int score = -1;

    public EndScreenUI endScreenUI;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();
        planesNum.text = "Zosta³o balonów: ";
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = "SCORE: " + score.ToString();
        
        if (score == 9999)//do zmiany
        {
            endScreenUI.Setup();
        }
    }
    public int getScore()
    {
        return score;
    }

    public void updateBalloonNum()
    {
        planesNum.text = "Zosta³o balonów: " + DestroyBalloon.balloonNum.ToString();
    }
}
