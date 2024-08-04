using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image fillerImage;
    [SerializeField] private bool canFade;
    [SerializeField] private CanvasGroup canvasGroup;

    private Sequence _healthBarFadeInSequence;
    private Sequence _healthBarFadeOutSequence;

    public void InitializeHealthBar()
    {
        ResetHealthBarController();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        var calculatedFillAmount = currentHealth / maxHealth;
        fillerImage.fillAmount = calculatedFillAmount;

        if (canFade)
        {
            _healthBarFadeInSequence?.Kill();
            _healthBarFadeOutSequence?.Kill();

            _healthBarFadeInSequence = DOTween.Sequence()
                .Join(canvasGroup.DOFade(1f, 0.35f))
                .AppendInterval(0.75f)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    _healthBarFadeOutSequence = DOTween.Sequence()
                        .Join(canvasGroup.DOFade(0f, 0.35f))
                        .SetEase(Ease.InOutSine);
                });
        }
    }

    public void ResetHealthBarController()
    {
        _healthBarFadeInSequence?.Kill();
        _healthBarFadeOutSequence?.Kill();

        if (fillerImage != null) fillerImage.fillAmount = 1f;
        if (canFade && canvasGroup != null) canvasGroup.alpha = 0f;
    }

    public void DisableHealthBar()
    {
        canvasGroup.alpha = 0f;
    }

    public void SetCanFade(bool value)
    {
        canFade = value;
        if (canFade)
        {
            _healthBarFadeInSequence?.Kill();
            _healthBarFadeOutSequence?.Kill();
            canvasGroup.alpha = 0f;
        }
        else
        {
            _healthBarFadeInSequence?.Kill();
            _healthBarFadeOutSequence?.Kill();
            canvasGroup.alpha = 1f;
        }
    }
}