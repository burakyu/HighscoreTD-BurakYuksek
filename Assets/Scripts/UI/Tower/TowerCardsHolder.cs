using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerCardsHolder : MonoBehaviour
{
    private List<TowerCard> _towerCards;

    private void OnEnable()
    {
        _towerCards = GetComponentsInChildren<TowerCard>().ToList();
        EventManager.TowerCardSelected.AddListener(UpdateCards);
        EventManager.TowerPlaced.AddListener(UnselectAll);

    }

    private void OnDisable()
    {
        EventManager.TowerCardSelected.RemoveListener(UpdateCards);
        EventManager.TowerPlaced.AddListener(UnselectAll);
    }

    private void UpdateCards(TowerType selectedType)
    {
        foreach (var towerCard in _towerCards)
        {
            if (towerCard.TowerType != selectedType)
                towerCard.Unselect();
        }
    }
    
    private void UnselectAll(GridPlacableTower tower = null, TowerGrid grid = null)
    {
        foreach (var towerCard in _towerCards)
            towerCard.Unselect();
    }
}
