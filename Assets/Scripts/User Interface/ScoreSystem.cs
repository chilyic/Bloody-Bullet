using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static int score = 10;
    public static int totalScore;
    public static Text scoreText;
    

    private void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = $"Score = {score}";
        totalScore = score;
    }
    
}
