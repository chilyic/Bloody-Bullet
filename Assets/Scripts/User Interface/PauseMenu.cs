using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
        InstBullet.canShoot = true;
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
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
