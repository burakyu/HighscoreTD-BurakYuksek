using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PriceHolderUI : MonoBehaviour
{
    [SerializeField] private Image currencyImage;
    [SerializeField] private TMP_Text priceText;

    public void SetCurrenyIcon()
    {
        //currencyImage.sprite = 
    }
    
    public void SetPriceText(int price)
    {
        priceText.text = price.ToString();
    }
}
