using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private AnimationClip _reloadClip;
    [SerializeField] private GameObject _magazine;
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _camera;
    [SerializeField] private Interface _interface;
    [SerializeField] private InstBullet _instBullet;

    private bool _isDropMag = false;
    private Vector3 _moveVector;
    private static CharacterController _controller;
    public static Animator animator;
    private Vector3 _camForward;
    private Vector3 _moveInput;
    private float _forwardAmount;
    private float _strafeAmount;
    private AudioSource _audio;
    public static bool isLife = true;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        _moveVector = Vector3.zero;
        _controller = GetComponent<CharacterController>();
        _audio = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (isLife)
        {
            _moveVector.x = Input.GetAxis("Horizontal");
            _moveVector.z = Input.GetAxis("Vertical");
            _moveVector.y -= 1;

            if (_controller.isGrounded)
            {
                _moveVector.y = 0;
                _camForward = Vector3.Scale(_camera.up, new Vector3(1, 0, 1)).normalized;
                _moveVector = _moveVector.z * _camForward + _moveVector.x * _camera.right;
            }
            else
                _moveVector.y -= 1 * Time.deltaTime;

            Move(_moveVector);

            _controller.Move(_moveVector * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.R) && !_isDropMag && InstBullet.totalAmmo > 0 && InstBullet.ammo < InstBullet.fullMag)
            {
                StartCoroutine(Reload());
                StartCoroutine(_instBullet.Reload());
            }
        }
    }

    private void Move(Vector3 move)
    {
        if (_moveVector.magnitude > 1)
            _moveVector.Normalize();
        
        _moveInput = move;

        CorrectAnimations();
    }

    void CorrectAnimations()
    {
        Vector3 localMove = transform.InverseTransformDirection(_moveInput);

        _strafeAmount = localMove.x;
        _forwardAmount = localMove.z;
        localMove.y = -1;
    
        animator.SetFloat("Run", _forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Strafe", _strafeAmount, 0.1f, Time.deltaTime);
    }
    
    IEnumerator Reload()
    {
        animator.Play("Reload");
        _audio.Play();
        _isDropMag = true;
        yield return new WaitForSeconds(0.5f);
        Instantiate(_magazine, _hand.transform.position, _hand.transform.rotation);
        yield return new WaitForSeconds(_reloadClip.length - 0.5f);
        _isDropMag = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Attack") || collision.gameObject.CompareTag("Axe"))
        {
            if (isLife)
                animator.Play("Reaction");
            
            if (_interface.healthSlider.value <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }

    public IEnumerator Death()
    {
        isLife = false;
        animator.Play("Death");

        yield return new WaitForSeconds(3);        
        _interface.EndGameScreen(ResultScreen.lose);
    }
}
