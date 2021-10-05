using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private AudioClip[] _Fx;    

    public void OnEnter()
    {
        _sound.PlayOneShot(_Fx[0]);
    }

    public void OnClic()
    {
        _sound.PlayOneShot(_Fx[1]);
    }
}
