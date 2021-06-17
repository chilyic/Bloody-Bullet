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

    public Animator animator;
    public float speed = 3;
    public float health = 100;
    public float armor = 0;
    public int point = 3;
    public float damage = 10;
    public float delayTime = 2;
    public bool canMove = true;
    public GameObject _target;
    public NavMeshAgent agent;

    void Start()
    {
        _target = GameObject.FindWithTag("Player");
        agent.enabled = true;
        agent.destination = _target.transform.position;
    }

    private void Update()
    {
        if (health > 0 && canMove)
        {
            this.transform.LookAt(_target.transform);
            if (PlayerController.isLife)
            {
                animator.SetFloat("Move", speed, 0.1f, Time.deltaTime);

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
            health -= _bullet.damage - _bullet.damage / 100 * armor;
            
            if (health <= 0)
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
        ScoreSystem.score += point;
        ScoreSystem.totalScore += point;
        ScoreSystem.scoreText.text = $"Score = {ScoreSystem.score}";
        Destroy(gameObject, delayTime);
    }
}
