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
    }

    private void OnDisable()
    {
        EventManager.TowerCardSelected.RemoveListener(UpdateCards);
    }

    private void UpdateCards(TowerType selectedType)
    {
        foreach (var towerCard in _towerCards)
        {
            if (towerCard.TowerType != selectedType)
                towerCard.Unselect();
        }
    }
}
