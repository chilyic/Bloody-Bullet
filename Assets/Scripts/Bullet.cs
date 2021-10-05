using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private ParticleSystem _sparks;
    [SerializeField] private ParticleSystem _stoneDust;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float _speed = 15f;

    public float damage = 20f;

    private void Start()
    {
        Destroy(gameObject, 1);
    }
    private void Update()
    { 
        transform.Translate(new Vector3(0, 0, _speed * Time.deltaTime));        
    }

    private void OnCollisionEnter(Collision other)
    {
        _mesh.enabled = false;
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            _audioSource.PlayOneShot(_audioClips[0]);
            _explosion.SetActive(true);
        }

        if (other.gameObject.CompareTag("Metall"))
            _audioSource.PlayOneShot(_audioClips[1]);

        if (other.gameObject.CompareTag("Wall"))
        {
            _audioSource.PlayOneShot(_audioClips[2]);
            _stoneDust.Play();
        }
        
        if (other.gameObject.CompareTag("Axe"))
        {
            _audioSource.PlayOneShot(_audioClips[3]);
            _sparks.Play();
        }
    }

    
}
