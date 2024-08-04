using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BomberProjectile))]
public class BombExplodeController : MonoBehaviour
{
    [SerializeField] private LayerMask projectileHitLayers;
    [SerializeField] private float bombExplodeRange;
    [SerializeField] private Transform bombModel;
    [SerializeField] private ParticleSystem explodeParticle;

    private BomberProjectile _bomberProjectile;
    private Collider[] _targetsOnRangeColliders = new Collider[15];
    private int _hitCount;

    private void Awake()
    {
        _bomberProjectile = GetComponent<BomberProjectile>();
    }

    private void OnEnable()
    {
        bombModel.gameObject.SetActive(true);
    }

    public IEnumerator Explode()
    {
        bombModel.gameObject.SetActive(false);
        explodeParticle.Play();
        _hitCount = Physics.OverlapSphereNonAlloc(transform.position, bombExplodeRange, _targetsOnRangeColliders,
            projectileHitLayers);

        for (int i = 0; i < _hitCount; i++)
        {
            int damageValue = (int) (100 / Vector3.Distance(transform.position, _targetsOnRangeColliders[i].transform.position));
            _targetsOnRangeColliders[i].GetComponent<CharacterHealthController>().ReceiveDamage(damageValue);
        }

        yield return new WaitForSeconds(1);
        
        _bomberProjectile.ResetAndDespawn();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bombExplodeRange);
    }
}
