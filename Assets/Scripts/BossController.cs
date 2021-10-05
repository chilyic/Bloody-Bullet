using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Collider _col;
    [SerializeField] private BossAttack _bossAttack;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _distanceToTarget = 4.5f;
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _health = 500;
    [SerializeField] private float _armor = 50;
    [SerializeField] private int _point = 30;
    [SerializeField] private float _delayTime = 7;

    public float damage = 35;
    public Animator animator;
    public NavMeshAgent agent;
    public static int bossesKilled;
    public static bool canMove = true;
    private GameObject _target;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player");
        agent.enabled = true;
        agent.destination = _target.transform.position;
        bossesKilled = 0;
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
                    StartCoroutine(_bossAttack.Attack());
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _health -= _bullet.damage - _bullet.damage / 100 * _armor;

            if (_health <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }

    private IEnumerator Death()
    {
        agent.enabled = false;
        canMove = false;
        _col.isTrigger = true;
        BossAttack.punchCollider.enabled = false;
        animator.Play("Death");
        ScoreSystem.score += _point;
        SpawnSystem.mobsSum = 0;
        bossesKilled++;

        yield return new WaitForSeconds(_delayTime);        
        Destroy(gameObject);
    }
}
