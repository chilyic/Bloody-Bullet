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
    [SerializeField] private AudioClip _noAmmoClip;
    [SerializeField] private AudioClip _ShotClip;

    public static bool canShoot;
    public static int ammo;
    public static int totalAmmo;
    public static int fullMag;
    private bool _plusAmmo = true;

    private void Start()
    {
        canShoot = true;
        ammo = 50;
        fullMag = ammo;
        totalAmmo = 100;
        _interface.ammoText.text = $"{totalAmmo}";
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
                    if (Input.GetMouseButtonDown(0))
                    _audio.PlayOneShot(_noAmmoClip);
            }

            if (totalAmmo == 0 && ammo == 0)
            {
                StartCoroutine(PlusAmmo());
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
        _audio.PlayOneShot(_ShotClip);
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

        _interface.ammoText.text = $"{totalAmmo}";
        _interface.ammoSlider.value = ammo;
        canShoot = true;
    }

    private IEnumerator PlusAmmo()
    {
        while (ammo < 20 && _plusAmmo)
        {
            _plusAmmo = false;
            yield return new WaitForSeconds(0.5f);
            ammo++;
            _interface.ammoText.text = $"{totalAmmo}";
            _interface.ammoSlider.value = ammo;
            _plusAmmo = true;            
        }
    }
}
