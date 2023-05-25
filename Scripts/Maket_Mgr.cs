using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Maket_Mgr : MonoBehaviour
{
    public static int BestScore = 0;
    public static int CurScore = 0;

    int cropScore = 300;
    public int bonusScore = 5000;
    int scoreSum = 0;

    public TextMeshProUGUI ScoreUI = null;
    void Start()
    {
        CurScore = 0;
    }

    void Update()
    {
        if(BestScore < CurScore)
        {
            BestScore = CurScore;
            changeBestScore();
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            calScore();
            UpdateScore(scoreSum);
            scoreSum = 0;
            PlayerController.Inst.cropNumSum = 0;
        }
    }
    void calScore()
    {

        scoreSum += cropScore * PlayerController.Inst.cropNumSum;
        if (PlayerController.Inst.checkIsFull())
        {
            scoreSum += bonusScore;
        }
    }

    public void UpdateScore(int score)
    {
        CurScore += score;
        PlayerPrefs.SetInt("CurScore", CurScore);
        ScoreUI.text = CurScore.ToString();
    }

    public void changeBestScore()
    {
        PlayerPrefs.SetInt("BestScore", BestScore);
    }

    public static void Load()
    {
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

}
