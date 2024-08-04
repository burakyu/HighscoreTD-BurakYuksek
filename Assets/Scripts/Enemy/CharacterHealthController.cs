using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterHealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private float shakeCooldown = 0.1f;
    [SerializeField] private Color bloodHitParticleColor;
    [SerializeField] private Color deathParticleColor;
    [SerializeField] private float health;

    private float _currentHealth;
    private Collider _collider;
    private bool _getsDamage = false;
    private bool _canShake = true;
    private Sequence _shakeSequence;
    private float _shakeCooldownTimer;
    private float _getsDamageCheckCooldownTimer;
    private HealthBarController _healthBarController;
    private Animator _characterMesh;

    public event Action OnCompletelyDestroyedEvent;
    bool IDamageable.GetsDamage { get => _getsDamage; set => _getsDamage = value; }
    public event Action<float, Vector3, GameObject> OnReceivedDamage;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _healthBarController = GetComponentInChildren<HealthBarController>();
        _characterMesh = GetComponentInChildren<Animator>();
        if (_healthBarController != null) _healthBarController.InitializeHealthBar();
    }
    
    void Update()
    {
        if (_shakeCooldownTimer > 0)
            _shakeCooldownTimer -= Time.deltaTime;
        else
            _canShake = true;

        if (_getsDamageCheckCooldownTimer > 0)
            _getsDamageCheckCooldownTimer -= Time.deltaTime;
        else
            _getsDamage = false;
    }

    private void OnReceiveDamage(float damage, Vector3 dir)
    {
        _healthBarController.UpdateHealthBar(_currentHealth, health);
        Shake(damage, dir);
    }

    private void OnDeath()
    {
        _collider.enabled = false;
        _getsDamage = false;
    }

    public void DieCompletely()
    {
        OnCompletelyDestroyedEvent?.Invoke();

        // pool despawn
        gameObject.SetActive(false);
    }

    private void Shake(float damage, Vector3 dir)
    {
        if (!_canShake) return;

        _shakeCooldownTimer = shakeCooldown;
        _canShake = false;

        OnReceivedDamage?.Invoke(damage, dir, this.gameObject);

        _shakeSequence?.Kill(true);
        _shakeSequence = DOTween.Sequence()
            .Join(_characterMesh.transform.DOPunchScale(-Vector3.one * 0.25f, 0.2f, 5, 0.1f));

        PoolManager.Instance.Spawn(Pools.Types.BloodParticle, transform.position, Quaternion.identity);
    }

    public IEnumerator GetDamage(float damage, float delay)
    {
        _getsDamage = true;
        _getsDamageCheckCooldownTimer = delay;
        yield return new WaitForSeconds(delay);
        _getsDamage = false;
    }
}
