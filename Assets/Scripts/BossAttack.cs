using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private AnimationClip _punchClip;
    [SerializeField] private ParticleSystem _blood;
    [SerializeField] private BossController _bossController;
    
    public static Collider punchCollider;
    private Interface _interface;

    void Start()
    {
        _interface = FindObjectOfType<Interface>();
    }
    public IEnumerator Attack()
    {
        BossController.canMove = false;
        _bossController.agent.enabled = false;
        punchCollider.isTrigger = true;
        _bossController.animator.Play("Attack");

        yield return new WaitForSeconds(_punchClip.length);
        punchCollider.isTrigger = false;
        BossController.canMove = true;
        _bossController.agent.enabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            punchCollider.isTrigger = false;
            _blood.Play();
            _interface.healthSlider.value -= _bossController.damage;
        }
    }
}
