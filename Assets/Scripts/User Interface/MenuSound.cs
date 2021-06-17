using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip[] Fx;
    

    public void OnEnter()
    {
        sound.PlayOneShot(Fx[0]);
    }

    public void OnClic()
    {
        sound.PlayOneShot(Fx[1]);
    }
}
