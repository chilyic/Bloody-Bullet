using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStarter : MonoBehaviour
{
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject[] _supplies;
    [SerializeField] private SpawnSystem _spawnSystem;
    [SerializeField] private SpawnPoint _spawnPoint;
    [SerializeField] private MusicController _music;
    [SerializeField] private int _mobs = 3;
    [SerializeField] private int _minTime = 2;
    [SerializeField] private int _maxTime = 3;
    [SerializeField] private int _waveForBoss = 4;

    public static int curWave;

    private void Start()
    {
        curWave = 0;

        Instantiate(_supplies[0], new Vector3(-7, 0.1f, 1f), Quaternion.identity);
        Instantiate(_supplies[0], new Vector3(7, 0.1f, 1f), Quaternion.identity);
        Instantiate(_supplies[1], new Vector3(-7, 0.1f, -3f), Quaternion.identity);
        Instantiate(_supplies[1], new Vector3(7, 0.1f, -3f), Quaternion.identity);

        Invoke(nameof(NextWave), 5);        
    }
    
    public void NextWave()
    {
        curWave++;
        Debug.Log(curWave);
        _music.PlayClip(curWave);

        if (curWave % _waveForBoss != 0)
            _spawnSystem.NewWave(_mobs, curWave, _minTime, _maxTime);
        else
        {
            BossWave();
            curWave = 0;
        }

        _mobs += 2;
    }

    private void BossWave()
    {
        Instantiate(_boss, transform.position, Quaternion.identity);
        _spawnSystem.NewWave(100, curWave, _minTime * 2, _maxTime * 2);
    }
}
