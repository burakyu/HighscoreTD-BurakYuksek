using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class HealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;

    private float _currentHealth;
    private Collider _collider;
    private Sequence _shakeSequence;
    private HealthBarController _healthBarController;
    
    public event Action<float, GameObject> OnReceivedDamage;
    public event Action OnDeath;
    
    protected virtual void Awake()
    {
        _collider = GetComponent<Collider>();
        _healthBarController = GetComponentInChildren<HealthBarController>();
        if (_healthBarController != null) _healthBarController.InitializeHealthBar();
    }

    private void OnEnable()
    {
        _collider.enabled = true;
        SetCurrentHealth(health);
    }

    private void SetCurrentHealth(float newHealth)
    {
        _currentHealth = Mathf.Clamp(newHealth, 0, health);
        if (_currentHealth <= 0.001f)
        {
            _currentHealth = 0f;
            Die();
        }
    }

    public virtual void ReceiveDamage(float damage)
    {
        SetCurrentHealth(_currentHealth - damage);
        _healthBarController.UpdateHealthBar(_currentHealth, health);
        Shake(damage);
    }

    protected virtual void Die()
    {
        _collider.enabled = false;
        OnDeath?.Invoke();
    }

    protected virtual void Shake(float damage)
    {
        OnReceivedDamage?.Invoke(damage, gameObject);

        _shakeSequence?.Kill(true);
        _shakeSequence = DOTween.Sequence()
            .Join(transform.DOPunchScale(-Vector3.one * 0.25f, 0.2f, 5, 0.1f));
    }
}