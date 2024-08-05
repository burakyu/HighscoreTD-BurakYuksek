using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    private MainTowerHealthController _towerHealthController;

    private void Awake()
    {
        _towerHealthController = GetComponent<MainTowerHealthController>();
    }

    private void OnEnable()
    {
        _towerHealthController.OnDeath += MainTowerLosed;
    }

    private void OnDisable()
    {
        _towerHealthController.OnDeath -= MainTowerLosed;
    }

    private void MainTowerLosed()
    {
        EventManager.MainTowerLosed.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Enemy enemy);
        if (enemy != null)
        {
            enemy.GetComponent<Collider>().enabled = false;
            _towerHealthController.ReceiveDamage(10);
            
            //despawn enemy
        }
    }
}
