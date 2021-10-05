using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audio;
    
    void Start()
    {
        _audio[0].Play();
    }

    public void PlayClip(int wave)
    {
        _audio[wave - 1].Stop();
        _audio[wave].Play();
    }

    public void StopClip()
    {
        for (int i = 0; i < _audio.Length; i++)
            _audio[i].Stop();
    }
}
