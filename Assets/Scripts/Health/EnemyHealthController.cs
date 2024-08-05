using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyHealthController : HealthController
{
    [SerializeField] private Pools.Types characterPoolType;
    
    private Animator _characterAnimator;
    private NavMeshAgent _navMeshAgent;

    protected override void Awake()
    {
        base.Awake();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _characterAnimator = GetComponentInChildren<Animator>();
    }
    
    public override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);
        
        if (damage > 100) damage = 100;
        ResourceManager.Instance.AddResource(ResourceType.GameScore, (int)(damage/3));
        ResourceManager.Instance.AddResource(ResourceType.GoldCoin, (int) (damage/5));
    }

    protected override void Die()
    {
        base.Die();
        _navMeshAgent.isStopped = true;
        _characterAnimator.SetTrigger("Die");
        EventManager.AddBoost.Invoke(GameManager.Instance.GameSettings.BoosterIncreaseValuePerEnemy);
        DOVirtual.DelayedCall(1.5f, () =>
        {
            PoolManager.Instance.Despawn(characterPoolType, gameObject);
        });
    }

    protected override void Shake(float damage)
    {
        base.Shake(damage);
        
        GameObject bloodParticle = PoolManager.Instance.Spawn(Pools.Types.BloodParticle, transform.position, Quaternion.identity);

        DOVirtual.DelayedCall(1, () =>
        {
            PoolManager.Instance.Despawn(Pools.Types.BloodParticle, bloodParticle);
        });
    }
}
