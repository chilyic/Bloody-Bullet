using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Collider _collider;
    [SerializeField] string _boxName;

    private Interface _interface;
    private SuppliesAudio _audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _interface = FindObjectOfType<Interface>();
            _audio = FindObjectOfType<SuppliesAudio>();

            if (_boxName == "Ammo")
            {
                InstBullet.totalAmmo += 50;
                _interface.ammoText.text = $"{InstBullet.totalAmmo}";
                _audio.PlayAmmoSound();
                TakeBox();
            }

            if (_boxName == "Medkit")
            {
                if (_interface.healthSlider.value < _interface.healthSlider.maxValue)
                {
                    _interface.healthSlider.value += 30;
                    _audio.PlayMedSound();
                    TakeBox();
                }
            }
        }
    }

    private void TakeBox()
    {
        _anim.SetTrigger("Open");
        _collider.enabled = false;
        Destroy(gameObject, 2);
    }
}
