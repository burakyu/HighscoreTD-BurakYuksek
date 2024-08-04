using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : BaseSingleton<ResourceManager>
{
    [SerializeField]
    private List<ResourceTypeResourceInfoEntry> resourceInfos = new List<ResourceTypeResourceInfoEntry>();

    private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();
    private ResourceControllerSaveData _saveData;
    
    public Dictionary<ResourceType, int> Resources => _resources;

    private void Start()
    {
        _resources.Add(ResourceType.GoldCoin, 0);
        LoadResourceData();
    }
    
    public void AddResource(ResourceType resourceType, int amount, bool updateUI = true, bool collectedOnUnderwater = false)
    {
        if (!_resources.ContainsKey(resourceType))
            _resources.Add(resourceType, amount);
        else
            _resources[resourceType] += amount;

        if (updateUI)
            GetCollectableDataFromType(resourceType).SetUI(_resources[resourceType]);

        EventManager.ResourceValuesChanged.Invoke();
        SaveResourceData();
    }

    public int TryTakeResources(ResourceType resourceType, int amount, bool updateUI = true)
    {
        if (!_resources.ContainsKey(resourceType) || _resources[resourceType] == 0)
            return 0;
        
        if (_resources[resourceType] < amount)
        {
            int result = _resources[resourceType];
            _resources[resourceType] = 0;

            if (updateUI) UpdateUI(resourceType);
            EventManager.ResourceValuesChanged.Invoke();
            SaveResourceData();
            return result;
        }
        else
        {
            _resources[resourceType] -= amount;

            if (updateUI) UpdateUI(resourceType);
            EventManager.ResourceValuesChanged.Invoke();
            SaveResourceData();
            return amount;
        }
    }
    
    public bool HasEnoughResource(ResourceType resourceType, int amount)
    {
        return _resources.ContainsKey(resourceType) && _resources[resourceType] >= amount;
    }

    public bool HasEnoughResource(ResourceAmountPair neededResources)
    {
        return HasEnoughResource(neededResources.collectableType, neededResources.amount);
    }
    
    private void LoadResourceData()
    {
        // if (SaveManager.Instance.LoadSaveData<Dictionary<ResourceType, int>>("ResourceData") != null)
        // {
        //     foreach (var resourceData in SaveManager.Instance.LoadSaveData<Dictionary<ResourceType, int>>("ResourceData"))
        //     {
        //         Player.Instance.ResourceBag.Resources.Add(resourceData.Key, resourceData.Value);
        //     }
        //     UpdateUI();
        // }
    }

    public void SaveResourceData()
    {
        //_saveData.ResourceTypesAndAmounts = _resources;
        //SaveManager.Instance.SaveData("ResourceData", Player.Instance.ResourceBag.Resources);
    }

    public ResourceInfo GetCollectableDataFromType(ResourceType type)
    {
        return resourceInfos[((int)type)-1].resourceInfo;
    }

    public void UpdateAllResourceUI()
    {
        foreach (var item in Resources)
        {
            UpdateUI(item.Key);
        }
    }

    public void UpdateUI(ResourceType certainType)
    {
        GetCollectableDataFromType(certainType).SetUI(Resources[certainType]);
    }
}

[System.Serializable]
public class ResourceTypeResourceInfoEntry
{
    public ResourceType resourceType;
    public ResourceInfo resourceInfo;
}

[System.Serializable]
public class ResourceInfo
{
    public Sprite resourceSprite;
    public ResourceUI[] resourceUi;

    public void SetUI(int amount)
    {
        foreach (var ui in resourceUi)
        {
            if (ui == null)
                return;

            if (!ui.gameObject.activeSelf)
            {
                ui.gameObject.SetActive(true);
            }
            
            ui.amountText.text = amount.ToString();
        }

    }
}

[System.Serializable]
public class ResourceControllerSaveData
{
    public Dictionary<ResourceType, int> ResourceTypesAndAmounts;

    public ResourceControllerSaveData()
    {
        ResourceTypesAndAmounts = new Dictionary<ResourceType, int>();
    }
}

public enum ResourceType
{
    None,
    GameScore,
    GoldCoin,
}