using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterHealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;

    private float _currentHealth;
    private Collider _collider;
    private Sequence _shakeSequence;
    private HealthBarController _healthBarController;
    private Animator _characterAnimator;
    public event Action<float, GameObject> OnReceivedDamage;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _healthBarController = GetComponentInChildren<HealthBarController>();
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
    }

    private void OnDeath()
    {
        // pool despawn
        _collider.enabled = false;
    }

    private void Shake(float damage)
    {
        OnReceivedDamage?.Invoke(damage, this.gameObject);

        _shakeSequence?.Kill(true);
        _shakeSequence = DOTween.Sequence()
            .Join(_characterAnimator.transform.DOPunchScale(-Vector3.one * 0.25f, 0.2f, 5, 0.1f));

        PoolManager.Instance.Spawn(Pools.Types.BloodParticle, transform.position, Quaternion.identity);
    }
}
