using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public Slider ammoSlider;
    public Text ammoText;
    public Slider healthSlider;
    public GameObject scorePanel;
    public ScoreSystem scoreSystem;
    public GameObject resultPanel;
    public ResultScreen resultScreen;
    public GameObject shopPanel;
    public float shopOpenTime = 20;
    public float shopCloseTime = 8;

    private void Start()
    {
        shopPanel.SetActive(false);
        resultPanel.SetActive(false);
        StartCoroutine(OpenShop());
    }

    IEnumerator OpenShop()
    {
        yield return new WaitForSeconds(shopOpenTime);
        shopPanel.SetActive(true);

        yield return new WaitForSeconds(shopCloseTime);
        shopPanel.SetActive(false);
        InstBullet.canShoot = true;
        StartCoroutine(OpenShop());
    }

    public void EndGameScreen(string endText)
    {
        resultPanel.SetActive(true);
        resultScreen.winOrLoseText.text = endText;
        PlayerController.animator.SetFloat("Run", 0);
        PlayerController.animator.SetFloat("Strafe", 0);
    }
}

