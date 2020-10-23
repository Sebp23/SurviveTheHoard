using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScore : MonoBehaviour
{
    //TODO high score
    private int highScore;

    private Score scoreScript;
    private TextMesh scoreText;
    private TextMesh highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        

        scoreText = GameObject.Find("ScoreText").GetComponent<TextMesh>();
        highScoreText = GameObject.Find("HighScore").GetComponent<TextMesh>();

        DetermineScores();

        //highScoreText.text = ($"High Score: {highScore}");
        highScoreText.text = ($"High Score: {PlayerPrefs.GetInt("HighScore", 0).ToString()}");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DetermineScores()
    {
        int currentScore = Score.score;
        scoreText.text = ($"Score: {currentScore}");

        if(currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScoreText.text = ($"High Score: {currentScore.ToString()}");
        }
    }
}