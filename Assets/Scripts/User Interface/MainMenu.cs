using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
    private float _progress;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        StartCoroutine(AsyncLoad());
        PlayerController.isLife = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            _progress = operation.progress / 0.9f;
            _scrollbar.size = _progress;
            yield return null;
        }
    }
}
