using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Collider _col;
    [SerializeField] private EnemyPunch _ep;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _distanceToTarget = 1.4f;
    [SerializeField] private GameObject[] _boxes;
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _health = 100;
    [SerializeField] private float _armor = 0;
    [SerializeField] private int _point = 3;
    [SerializeField] private float _destroyDelayTime = 2;

    public NavMeshAgent agent;
    public Animator animator;    
    public float damage = 10;    
    public bool canMove = true;

    private GameObject _target;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player");
        agent.enabled = true;
        agent.destination = _target.transform.position;
        agent.speed = _speed;
    }

    private void Update()
    {
        if (_health > 0 && canMove)
        {
            this.transform.LookAt(_target.transform);
            if (PlayerController.isLife)
            {
                animator.SetFloat("Move", _speed, 0.1f, Time.deltaTime);

                if (Vector3.Distance(transform.position, _target.transform.position) < _distanceToTarget)
                    StartCoroutine(_ep.Attack());
                else
                    agent.destination = _target.transform.position;
            }
            else
            {
                animator.SetFloat("Move", 0, 0.1f, Time.deltaTime);
                agent.destination = transform.position;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _health -= _bullet.damage - _bullet.damage / 100 * _armor;
            
            if (_health <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        agent.enabled = false;
        canMove = false;
        _col.isTrigger = true;
        _ep.punchCollider.enabled = false;
        animator.Play("Death");
        ScoreSystem.score += _point;
        ScoreSystem.scoreText.text = $"{ScoreSystem.score}";
        StartCoroutine(Dead());
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(_destroyDelayTime);
        Destroy(gameObject);
        int rand = Random.Range(0, 15);

        if (rand < 5)
            Instantiate(_boxes[0], transform.position, Quaternion.identity);
        if (rand == 14)
            Instantiate(_boxes[1], transform.position, Quaternion.identity);
    }
}
