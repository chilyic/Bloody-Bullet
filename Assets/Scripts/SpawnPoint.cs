using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] mobs;
    public int _lightMobs;
    public int _hardMobs;
    public int mobsCount;
    
    public void SpawnEnemy(int wave)
    {
        int rand = Random.Range(0, wave);
        
        if (rand == 0)
        {
            if(_lightMobs > 0)
                _lightMobs--;
            else
            {
                rand = 1;
                _hardMobs--;
            }
        }
        
        if (rand == 1)
        {
            if (_hardMobs > 0)
                _hardMobs--;
            else
            {
                rand = 0;
                _lightMobs--;
            }
        }

        Instantiate(mobs[rand], transform.position, Quaternion.identity);
        mobsCount--;
    }

    public void MobsData(int lightMobs, int hardMobs)
    {
        _lightMobs = lightMobs;
        _hardMobs = hardMobs;
        mobsCount = _lightMobs + _hardMobs;
    }
}
