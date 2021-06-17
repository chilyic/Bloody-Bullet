using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Collider _col;
    [SerializeField] private BossAttack _bossAttack;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _distanceToTarget = 4.5f;

    private Interface _interface;

    public Animator animator;
    public float speed = 2;
    public float health = 600;
    public float armor = 90;
    public int point = 50;
    public float damage = 35;
    public float delayTime = 7;
    public bool canMove = true;
    public GameObject _target;
    public NavMeshAgent agent;

    void Start()
    {
        _target = GameObject.FindWithTag("Player");
        agent.enabled = true;
        agent.destination = _target.transform.position;
        _interface = FindObjectOfType<Interface>();
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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health -= _bullet.damage - _bullet.damage / 100 * armor;

            if (health <= 0)
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
        _bossAttack.punchCollider.enabled = false;
        animator.Play("Death");
        ScoreSystem.score += point;
        ScoreSystem.totalScore += point;
        ScoreSystem.scoreText.text = $"Score = {ScoreSystem.score}";
        
        yield return new WaitForSeconds(delayTime);
        EndGame();
    }

    private void EndGame()
    {
        _interface.EndGameScreen(ResultScreen.win);
        _interface.StopAllCoroutines();
        PlayerController.isLife = false;
    }
}
