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

    private Tween _holderMoveTween;
    
    public TowerType TowerType => towerType;
    
    public void OnPointerDown(PointerEventData eventData)
    {
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

    private void UpdatePriceText(int price)
    {
        priceHolder.SetPriceText(price);
    }
}
