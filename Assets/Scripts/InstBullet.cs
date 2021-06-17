using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstBullet : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private ParticleSystem _flash;
    [SerializeField] private ParticleSystem _mainFlash;
    [SerializeField] private float _fireRate = 0.07f;
    [SerializeField] private AnimationClip _reloadClip;
    [SerializeField] private Interface _interface;
    [SerializeField] private AudioSource _audio;
    
    public AudioClip noAmmoClip;
    public static bool canShoot = true;
    public static int ammo = 40;
    public static int totalAmmo = 100;
    public static int fullMag;

    private void Start()
    {
        fullMag = ammo;
        totalAmmo = 100;
        _interface.ammoText.text = $"Ammo {totalAmmo}";
    }
    private void Update()
    {
        if (PlayerController.isLife)
        {
            if (Input.GetMouseButton(0) && canShoot)
            {
                if (ammo > 0)
                    StartCoroutine(Shoot());
                else
                    _audio.PlayOneShot(noAmmoClip);
            }
        }
    }
    private IEnumerator Shoot()
    {
        canShoot = false;
        Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation);
        PlayerController.animator.Play("Firing");
        _flash.Play();
        _mainFlash.Play();
        _audio.Play();
        ammo--;
        _interface.ammoSlider.value = ammo;

        yield return new WaitForSeconds(_fireRate);
        canShoot = true;        
    }

     public IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(_reloadClip.length);

        if (totalAmmo >= fullMag - ammo)
        {
            totalAmmo -= fullMag - ammo;
            ammo += fullMag - ammo;
        }
        else
        {
            ammo += totalAmmo;
            totalAmmo = 0;
        }
        
        _interface.ammoText.text = $"Ammo {totalAmmo}";
        _interface.ammoSlider.value = ammo;
        canShoot = true;
    }
}
