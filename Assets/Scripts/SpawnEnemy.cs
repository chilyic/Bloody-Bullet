using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] prefab;
    public int minTime = 2;
    public int maxTime = 4;
    public GameObject Hp;
    public GameObject canvas;
    int rand;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        rand = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(rand);
        Instantiate(prefab[Random.Range(0, prefab.Length)], transform.position, Quaternion.identity);
        StartCoroutine(Spawn());
    }
}
