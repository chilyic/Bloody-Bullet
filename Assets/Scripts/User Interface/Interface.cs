using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField] private MusicController _music;
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _resultPanel;

    public Slider healthSlider;
    public Slider ammoSlider;
    public Text ammoText;

    private void Start()
    {
        _resultPanel.SetActive(false);
    }

    public void EndGameScreen()
    {
        _music.StopClip();
        _music.PlayClip(5);
        _resultPanel.SetActive(true);
        PlayerController.animator.SetFloat("Run", 0);
        PlayerController.animator.SetFloat("Strafe", 0);
    }
}

