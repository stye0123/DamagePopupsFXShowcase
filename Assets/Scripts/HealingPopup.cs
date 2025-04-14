using UnityEngine;
using DG.Tweening;

public class HealingPopup : FloatingText
{
    private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 1.2f, 1, 1f);

    public void InitializeHealing(int amount, Color color, float size, float duration, float speed, Sprite background)
    {
        base.Initialize(amount.ToString(), color, size, duration, speed, 1f, background);
        
        // 初始縮放效果
        rectTransform.DOScale(Vector3.one * 1.2f, 0.2f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => {
                rectTransform.DOScale(Vector3.one, 0.3f)
                    .SetEase(Ease.InOutQuad);
            });
        
        if (backgroundImage != null && background != null)
        {
            backgroundImage.sprite = background;
            backgroundImage.enabled = true;
            backgroundImage.color = new Color(1, 1, 1, 0.6f);
            backgroundImage.DOFade(0f, duration).SetEase(Ease.InQuad);
        }
    }

    protected override void Update()
    {
        base.Update();
        
        // 根據生命週期調整縮放
        float scale = scaleCurve.Evaluate(progress);
        rectTransform.localScale = Vector3.one * scale;
    }
} 