using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BomberProjectile : MonoBehaviour
{
    [SerializeField] private TowerSettings ownerTowerSettings;

    private BombExplodeController bombExplodeController;

    public TowerSettings TowerSettings => ownerTowerSettings;
    
    private void Awake()
    {
        bombExplodeController = GetComponent<BombExplodeController>();
    }

    public void SendBomb(Vector3 targetPos)
    {
        transform.DOJump(targetPos, 3.5f, 1, 1.5f).OnComplete(()=>
        {
            StartCoroutine(bombExplodeController.Explode());
        });
    }
    
    public void ResetAndDespawn()
    {
        //ResetProjectile();
        PoolManager.Instance.Despawn(Pools.Types.BomberBomb, this.gameObject);
    }
}
