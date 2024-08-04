using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MinerTowerWeapon : TowerWeapon
{
    public override void Attack(Vector3 attackPoint)
    {
        Debug.LogError("Miner Attacked");
        transform.DOPunchScale(-transform.localScale * 0.1f, 0.25f, 10, 1);
    }

    public override void StopAttack()
    {
        
    }
}
