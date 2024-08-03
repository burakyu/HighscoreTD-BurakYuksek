using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowersLibrary", menuName = "HighscoreTD/Tower/TowersLibrary")]
public class TowersLibrary : ScriptableObject
{
    public List<TowerSettings> allTowers = new List<TowerSettings>();

    public TowerSettings GetTowerDataByType(TowerType towerType)
    {
        return allTowers.Find(c => c.TowerType == towerType);
    }
}
