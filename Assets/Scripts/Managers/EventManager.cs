using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<TowerType> TowerCardSelected = new UnityEvent<TowerType>();
    public static UnityEvent<Tower> TowerDragCanceled = new UnityEvent<Tower>();
    public static UnityEvent<GridPlacableTower, TowerGrid> TowerPlaced = new UnityEvent<GridPlacableTower, TowerGrid>();
    public static UnityEvent FirstTowerPlaced = new UnityEvent();
    public static UnityEvent ResourceValuesChanged = new UnityEvent();
    public static UnityEvent MainTowerLosed = new UnityEvent();
    public static UnityEvent RestartGame = new UnityEvent();
    public static UnityEvent<int> AddBoost = new UnityEvent<int>();
    public static UnityEvent BoostValueCompleted = new UnityEvent();
    public static UnityEvent BoostUsed = new UnityEvent();
}