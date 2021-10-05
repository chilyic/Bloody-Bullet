using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoint;
    [SerializeField] private WaveStarter _waveStarter;
    [SerializeField] private int _delayBetweenWaves = 20;

    private int _wave;
    private int _randTime;
    private int _randPoint;
    public static int mobsSum;
    
    private IEnumerator Spawn(int wave, int minTime, int maxTime)
    {
        _wave = wave;
        _randTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(_randTime);

        if (PlayerController.isLife)
            if (mobsSum > 0)
            {
                _randPoint = Random.Range(0, _spawnPoint.Length);
                while (_spawnPoint[_randPoint].mobsCount == 0)
                {
                    _randPoint = Random.Range(0, _spawnPoint.Length);
                }

                _spawnPoint[_randPoint].SpawnEnemy(_wave);
                mobsSum--;

                StartCoroutine(Spawn(_wave, minTime, maxTime));
            }
            else
            {
                StopAllCoroutines();
                Invoke(nameof(DelayNextWave), _delayBetweenWaves);
            }
    }


    public void NewWave(int mobs, int wave, int minTime, int maxTime)
    {
        mobsSum = mobs * _spawnPoint.Length;

        for (int i = 0; i < _spawnPoint.Length; i++)
        {
            _spawnPoint[i].MobsData(mobs);
        }
        StartCoroutine(Spawn(wave, minTime, maxTime));
    }

    private void DelayNextWave()
    {
        _waveStarter.NextWave();
    }
}
