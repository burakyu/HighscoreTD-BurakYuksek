using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MinerProjectile))]
public class MineExplodeController : MonoBehaviour
{
    [SerializeField] private LayerMask projectileHitLayers;
    [SerializeField] private float mineExplodeRange;
    [SerializeField] private Transform mineModel;
    [SerializeField] private ParticleSystem explodeParticle;

    private MinerProjectile _minerProjectile;
    private Collider[] _targetsOnRangeColliders = new Collider[15];
    private int _hitCount;

    public event Action MineExplode; 
    
    private void Awake()
    {
        _minerProjectile = GetComponent<MinerProjectile>();
    }

    private void OnEnable()
    {
        mineModel.gameObject.SetActive(true);
    }

    public IEnumerator Explode()
    {
        mineModel.gameObject.SetActive(false);
        explodeParticle.Play();
        _hitCount = Physics.OverlapSphereNonAlloc(transform.position, mineExplodeRange, _targetsOnRangeColliders,
            projectileHitLayers);

        for (int i = 0; i < _hitCount; i++)
        {
            int damageValue = (int) (150 / Vector3.Distance(transform.position, _targetsOnRangeColliders[i].transform.position));
            _targetsOnRangeColliders[i].GetComponent<CharacterHealthController>().ReceiveDamage(damageValue);
        }

        yield return new WaitForSeconds(1);
        
        MineExplode?.Invoke();        
        _minerProjectile.ResetAndDespawn();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, mineExplodeRange);
    }
}
