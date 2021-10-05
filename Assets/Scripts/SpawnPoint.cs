using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] _mobs;
    [HideInInspector] public int mobsCount;
    
    public void SpawnEnemy(int wave)
    {
        if (wave > 3)
            wave = 3;

        int rand = Random.Range(0, wave);
        
        Instantiate(_mobs[rand], transform.position, Quaternion.identity);
        mobsCount--;
    }

    public void MobsData(int mobs)
    {
        mobsCount = mobs;
    }
}
