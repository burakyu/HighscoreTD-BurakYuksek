using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSimpleTrigger : MonoBehaviour
{
    private MineExplodeController _mineExplodeController;

    private Collider _collider;
    
    private void Awake()
    {
        _mineExplodeController = GetComponent<MineExplodeController>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Enemy enemy);
        if (enemy != null)
        {
            _collider.enabled = false;
            StartCoroutine(_mineExplodeController.Explode());
        }
    }
}