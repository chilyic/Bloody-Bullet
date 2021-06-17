using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField]
    private Interface _interface;
    [SerializeField]
    private AudioSource _audio;
    public int ammoPrice = 10;
    public int healthPrice = 25;

    public void BuyAmmo()
    {
        if (ScoreSystem.score >= ammoPrice)
        {
            _audio.Play();
            ScoreSystem.score -= ammoPrice;
            InstBullet.totalAmmo += 50;
            _interface.ammoText.text = $"{InstBullet.totalAmmo}";
            ScoreSystem.scoreText.text = $"Score = {ScoreSystem.score}";
        }
    }

    public void BuyHealth()
    {
        if (ScoreSystem.score >= healthPrice)
        {
            _audio.Play();
            ScoreSystem.score -= healthPrice;
            _interface.healthSlider.value += 30;
            ScoreSystem.scoreText.text = $"Score = {ScoreSystem.score}";
        }
    }

    public void Close()
    {
        _audio.Play();
        this.gameObject.SetActive(false);
        InstBullet.canShoot = true;
    }

    public void OnMouseOver()
    {
        InstBullet.canShoot = false;
    }

    public void OnMouseExit()
    {
        InstBullet.canShoot = true;
    }
}
