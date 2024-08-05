using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerSettings", menuName = "HighscoreTD/Tower/TowerSettings")]
public class TowerSettings : ScriptableObject
{
    public TowerType TowerType;
    public float AttackRange;
    public int DamageValue;
    public float shootInterval;
    
    [Header("Custom Prices For Flexible Game Economy")]
    public List<int> TowerPricesByCount;
}
