using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private AnimationClip _punchClip;
    [SerializeField]
    private ParticleSystem _blood;

    public BossController bossController;
    public Collider punchCollider;
    private Interface _interface;

    void Start()
    {
        _interface = FindObjectOfType<Interface>();
    }
    public IEnumerator Attack()
    {
        bossController.canMove = false;
        bossController.agent.enabled = false;
        punchCollider.isTrigger = true;
        bossController.animator.Play("Attack");
        yield return new WaitForSeconds(_punchClip.length);
        punchCollider.isTrigger = false;
        bossController.canMove = true;
        bossController.agent.enabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            punchCollider.isTrigger = false;
            _blood.Play();
            _interface.healthSlider.value -= bossController.damage;
        }
    }
}
