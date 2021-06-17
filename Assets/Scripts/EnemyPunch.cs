using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{
    [SerializeField] private AnimationClip _punchClip;
    [SerializeField] private ParticleSystem _blood;
    
    public EnemyController ec;
    public Collider punchCollider;

    private Interface _interface;
    
    void Start()
    {
        _interface = FindObjectOfType<Interface>();
    }
    public IEnumerator Attack()
    {
        ec.canMove = false;
        ec.agent.enabled = false;
        punchCollider.isTrigger = true;
        ec.animator.Play("Attack");
        yield return new WaitForSeconds(_punchClip.length);
        punchCollider.isTrigger = false;
        ec.canMove = true;
        ec.agent.enabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            punchCollider.isTrigger = false;
            _blood.Play();
            _interface.healthSlider.value -= ec.damage;
        }
    }
}
