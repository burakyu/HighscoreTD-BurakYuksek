using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    [SerializeField] private TowersLibrary towersLibrary;

    public TowersLibrary TowersLibrary => towersLibrary;
}
