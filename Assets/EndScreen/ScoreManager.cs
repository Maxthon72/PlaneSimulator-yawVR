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
    int score = 0;

    public EndScreenUI endScreenUI;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = "SCORE: " + score.ToString();
        
        if (score == 5)//do zmiany
        {
            endScreenUI.Setup();
        }
    }
}
