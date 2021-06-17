using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoint;
    [SerializeField] private WaveStarter _waveStarter;    
    
    public int delayBetweenWaves = 20;

    private int _wave;
    private int _randTime;
    private int _randPoint;
    private int _mobsSum;
    
    private IEnumerator Spawn(int wave, int minTime, int maxTime)
    {
        _wave = wave;
        _randTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(_randTime);

        if (PlayerController.isLife)
            if (_mobsSum > 0)
            {
                _randPoint = Random.Range(0, _spawnPoint.Length);
                while (_spawnPoint[_randPoint].mobsCount == 0)
                {
                    _randPoint = Random.Range(0, _spawnPoint.Length);
                }

                _spawnPoint[_randPoint].SpawnEnemy(_wave);
                _mobsSum--;

                StartCoroutine(Spawn(_wave, minTime, maxTime));
            }
            else
            {
                StopAllCoroutines();
                Invoke(nameof(DelayNextWave), delayBetweenWaves);
            }
    }


    public void NewWave(int mobsLight, int mobsHard, int wave, int minTime, int maxTime)
    {
        _mobsSum = (mobsLight + mobsHard) * _spawnPoint.Length;

        for (int i = 0; i < _spawnPoint.Length; i++)
        {
            _spawnPoint[i].MobsData(mobsLight, mobsHard);
        }
        StartCoroutine(Spawn(wave, minTime, maxTime));
    }

    private void DelayNextWave()
    {
        _waveStarter.NextWave();
    }
}
