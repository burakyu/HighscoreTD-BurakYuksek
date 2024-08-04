using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSimpleTrigger : MonoBehaviour
{
    private MineExplodeController _mineExplodeController;

    private void Awake()
    {
        _mineExplodeController = GetComponent<MineExplodeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Enemy enemy);
        if (enemy != null)
        {
            StartCoroutine(_mineExplodeController.Explode());
        }
    }
}
