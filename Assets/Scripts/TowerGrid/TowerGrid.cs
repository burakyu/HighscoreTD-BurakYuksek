using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TowerGrid : MonoBehaviour
{
    [SerializeField] private Transform defaultGridModel;
    [SerializeField] private Transform previewGridModel;

    private GridPlacableTower _currentGridTower;

    public void SelectPreview(bool show)
    {
        if (_currentGridTower != null) return;
        
        defaultGridModel.gameObject.SetActive(!show);
        previewGridModel.gameObject.SetActive(show);
    }

    public bool PlaceToGrid(GridPlacableTower gridPlacableTower)
    {
        if (_currentGridTower != null) return false;
        
        SelectPreview(false);
        _currentGridTower = gridPlacableTower;
        gridPlacableTower.transform.parent = transform;
        EventManager.TowerPlaced.Invoke(gridPlacableTower, this);
        gridPlacableTower.transform.DOLocalJump(Vector3.zero, 2, 1, 0.25f)
            .OnComplete(() =>
            {
                
            });

        return true;
    }
}