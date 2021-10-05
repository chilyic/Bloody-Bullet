using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioSource _audio;

    public void PlayAmmoSound()
    {
        _audio.PlayOneShot(_clips[0]);
    }

    public void PlayMedSound()
    {
        _audio.PlayOneShot(_clips[1]);
    }
}
