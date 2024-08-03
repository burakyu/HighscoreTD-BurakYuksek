using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<TowerType> TowerCardSelected = new UnityEvent<TowerType>();
    public static UnityEvent<Tower> TowerDragCanceled = new UnityEvent<Tower>();
    public static UnityEvent<GridPlacableTower, TowerGrid> TowerPlaced = new UnityEvent<GridPlacableTower, TowerGrid>();
}