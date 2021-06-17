using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public Text winOrLoseText;
    public Text scoreText;
    public static string win = "You Win!";
    public static string lose = "You Lose...";

    [SerializeField] private AudioSource _audio;

    private void Start()
    {
        scoreText.text = $"Total Score = {ScoreSystem.totalScore}";
    }

    public void Restart()
    {
        _audio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerController.isLife = true;
        InstBullet.ammo = 40;
        ScoreSystem.score = 10;
    }

    public void MainMenu()
    {
        _audio.Play();
        SceneManager.LoadScene(0);
    }
}
