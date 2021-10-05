using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static int score;
    public static Text scoreText;    

    private void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
        scoreText.text = $"{score}";
    }
}
