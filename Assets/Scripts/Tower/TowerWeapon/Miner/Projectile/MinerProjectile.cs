using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MinerProjectile : MonoBehaviour
{
    [SerializeField] private TowerSettings ownerTowerSettings;
    [SerializeField] private Collider mineTriggerCollider;

    public TowerSettings TowerSettings => ownerTowerSettings;
    
    public void SendMine(Vector3 targetPos)
    {
        mineTriggerCollider.enabled = false;

        DOTween.Sequence()
            .Append(transform.DOJump(targetPos, 1f, 1, 0.5f))
            .AppendInterval(0.1f)
            .OnComplete(() => { mineTriggerCollider.enabled = true; });
    }

    public void ResetAndDespawn()
    {
        //ResetProjectile();
        PoolManager.Instance.Despawn(Pools.Types.MinerMine, this.gameObject);
    }
}