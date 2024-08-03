using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerSettings", menuName = "HighscoreTD/Tower/TowerSettings")]
public class TowerSettings : ScriptableObject
{
    public TowerType TowerType;
    public GameObject TowerPrefab;
    public List<int> TowerPricesByCount;
}
