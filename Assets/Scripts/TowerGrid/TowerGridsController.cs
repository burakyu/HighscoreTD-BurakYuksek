using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerGridsController : MonoBehaviour
{
    private List<TowerGrid> _towerGrids;

    private void Awake()
    {
        _towerGrids = GetComponentsInChildren<TowerGrid>().ToList();
    }

    private void OnEnable()
    {
        EventManager.TowerPlaced.AddListener(CheckFirstTower);
    }

    private void OnDisable()
    {
        EventManager.TowerPlaced.RemoveListener(CheckFirstTower);
    }

    private void CheckFirstTower(GridPlacableTower tower, TowerGrid grid)
    {
        List<TowerGrid> notEmptyGrids = _towerGrids.FindAll(x => x.HasTower());

        if (notEmptyGrids.Count <= 1)
        {
            EventManager.FirstTowerPlaced.Invoke();
        }
    }
}
