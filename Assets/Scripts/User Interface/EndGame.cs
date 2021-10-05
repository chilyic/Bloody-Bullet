using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private SaveScore _sv = new SaveScore();
    private string path;

    [SerializeField] private Text _waves;
    [SerializeField] private Text _bosses;
    [SerializeField] private Text _totalScore;
    [SerializeField] private Text _highScore;
    [SerializeField] private GameObject _newRecordPanel;

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "SaveScore.json");

        if (File.Exists(path))
        {
            _sv = JsonUtility.FromJson<SaveScore>(File.ReadAllText(path));
            _highScore.text = $"{_sv.highScore}";
        }
        else
            _highScore.text = "0";

        _waves.text = $"{WaveStarter.curWave - 1}";
        _bosses.text = $"{BossController.bossesKilled}";
        _totalScore.text = $"{ScoreSystem.score}";
        
        if (ScoreSystem.score > Convert.ToInt32(_highScore.text))
        {
            _highScore.text = $"{ScoreSystem.score}";
            _sv.highScore = ScoreSystem.score;
            _newRecordPanel.SetActive(true);
            Invoke(nameof(ClosePanel), 3);
            Save();
        }
    }

    private void ClosePanel() => _newRecordPanel.SetActive(false);

    private void Save()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            File.WriteAllText(path, JsonUtility.ToJson(_sv));
        }
        else
            File.WriteAllText(path, JsonUtility.ToJson(_sv));
    }

    public void Restart()
    {
        Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

[Serializable]
public class SaveScore
{
    public int highScore;
}
