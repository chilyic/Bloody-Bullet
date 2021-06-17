using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStarter : MonoBehaviour
{
    [SerializeField] private GameObject _boss;
    [SerializeField] private SpawnSystem _spawnSystem;
    [SerializeField] private SpawnPoint _spawnPoint;

    public int mobsLight = 10;
    public int mobsHard = 10;
    public int waveForBoss = 3;
    public int curWave;
    public int minTime = 2;
    public int maxTime = 3;
    void Start()
    {
        Invoke(nameof(FirstWave), 5);
    }
        
    void FirstWave()
    {
        curWave = 1;
        _spawnSystem.NewWave(mobsLight, 0, curWave, minTime, maxTime);
    }

    public void NextWave()
    {
        mobsLight += 5;
        mobsHard += 5;
        curWave++;

        if (curWave < waveForBoss)
            _spawnSystem.NewWave(mobsLight, mobsHard, curWave, minTime, maxTime);
        else
            BossWave();
        Debug.Log(curWave);
    }

    void BossWave()
    {
        Instantiate(_boss, transform.position, Quaternion.identity);
        _spawnSystem.NewWave(mobsLight, mobsHard, _spawnPoint.mobs.Length, minTime * 3, maxTime * 3);
    }
}
