using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TowerType towerType;
    [SerializeField] private RectTransform holder;
    [SerializeField] private Image selectedOutlineBg;
    [SerializeField] private PriceHolderUI priceHolder;
    [SerializeField] private RectTransform cardDisableItem;

    private Tween _holderMoveTween;
    private bool _canBeSelect;
    
    public TowerType TowerType => towerType;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => TowerManager.Instance != null && ResourceManager.Instance != null);
        CheckCurrencyAvailability();
        UpdatePriceText();
    }

    private void OnEnable()
    {
        EventManager.TowerPlaced.AddListener(UpdatePriceText);
        EventManager.ResourceValuesChanged.AddListener(CheckCurrencyAvailability);
    }

    private void OnDisable()
    {
        EventManager.TowerPlaced.RemoveListener(UpdatePriceText);
        EventManager.ResourceValuesChanged.AddListener(CheckCurrencyAvailability);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_canBeSelect) return;
        Select();
    }
    
    private void Select()
    {
        EventManager.TowerCardSelected.Invoke(towerType);
        
        selectedOutlineBg.gameObject.SetActive(true);
        
        _holderMoveTween?.Kill();
        _holderMoveTween = holder.DOAnchorPosY(100, 0.2f);
    }
    
    public void Unselect()
    {
        selectedOutlineBg.gameObject.SetActive(false);
        
        _holderMoveTween?.Kill();
        _holderMoveTween = holder.DOAnchorPosY(0, 0.2f);
    }
    
    private void UpdatePriceText(GridPlacableTower tower = null, TowerGrid grid = null)
    {
        int towerPrice = TowerManager.Instance.GetCurrentTowerPrice(towerType);
        priceHolder.SetPriceText(towerPrice);
    }

    private void CheckCurrencyAvailability()
    {
        int towerPrice = TowerManager.Instance.GetCurrentTowerPrice(towerType);
        bool available = ResourceManager.Instance.HasEnoughResource(ResourceType.GoldCoin, towerPrice);
        cardDisableItem.gameObject.SetActive(!available);
        SetCanSelect(available);
    }
    
    private void SetCanSelect(bool canBeSelect)
    {
        _canBeSelect = canBeSelect;
    }
}
