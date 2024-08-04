using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class CharacterHealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private Pools.Types characterPoolType;
    [SerializeField] private float health;

    private float _currentHealth;
    private Collider _collider;
    private Sequence _shakeSequence;
    private HealthBarController _healthBarController;
    private NavMeshAgent _navMeshAgent;
    private Animator _characterAnimator;
    public event Action<float, GameObject> OnReceivedDamage;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _healthBarController = GetComponentInChildren<HealthBarController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _characterAnimator = GetComponentInChildren<Animator>();
        if (_healthBarController != null) _healthBarController.InitializeHealthBar();
    }

    private void OnEnable()
    {
        SetCurrentHealth(health);
    }

    private void SetCurrentHealth(float newHealth)
    {
        _currentHealth = Mathf.Clamp(newHealth, 0, health);
        if (_currentHealth <= 0.001f)
        {
            _currentHealth = 0f;
            OnDeath();
        }
    }
    
    public void ReceiveDamage(float damage)
    {
        SetCurrentHealth(_currentHealth - damage);
        _healthBarController.UpdateHealthBar(_currentHealth, health);
        Shake(damage);

        if (damage > 100) damage = 100;
        ResourceManager.Instance.AddResource(ResourceType.GameScore, (int)(damage/3));
        ResourceManager.Instance.AddResource(ResourceType.GoldCoin, (int) (damage/5));
    }

    private void OnDeath()
    {
        // pool despawn
        _navMeshAgent.isStopped = true;
        _collider.enabled = false;
        _characterAnimator.SetTrigger("Die");
        DOVirtual.DelayedCall(1.5f, () =>
        {
            PoolManager.Instance.Despawn(characterPoolType, gameObject);
        });
    }

    private void Shake(float damage)
    {
        OnReceivedDamage?.Invoke(damage, gameObject);

        _shakeSequence?.Kill(true);
        _shakeSequence = DOTween.Sequence()
            .Join(_characterAnimator.transform.DOPunchScale(-Vector3.one * 0.25f, 0.2f, 5, 0.1f));

        GameObject bloodParticle = PoolManager.Instance.Spawn(Pools.Types.BloodParticle, transform.position, Quaternion.identity);

        DOVirtual.DelayedCall(1, () =>
        {
            PoolManager.Instance.Despawn(Pools.Types.BloodParticle, bloodParticle);
        });
    }
}
