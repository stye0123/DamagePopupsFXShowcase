using UnityEngine;
using DG.Tweening;

public class CriticalPopup : DamagePopup
{
    private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 1.5f, 1, 1f);


    public void InitializeCritical(int damage, Color color, float size, float duration, float speed, 
        float arcHeight, float scaleMultiplier, Sprite background, float horizontalOffset)
    {
        base.InitializeDamage(damage, color, size, duration, speed, arcHeight, horizontalOffset);
        
        // 添加額外的爆擊效果
        rectTransform.DOScale(Vector3.one * scaleMultiplier, 0.2f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => {
                rectTransform.DOScale(Vector3.one, 0.3f)
                    .SetEase(Ease.InOutQuad);
            });
        
        if (backgroundImage != null && background != null)
        {
            backgroundImage.sprite = background;
            backgroundImage.enabled = true;
            backgroundImage.color = new Color(1, 1, 1, 0.8f);
            backgroundImage.DOFade(0f, duration).SetEase(Ease.InQuad);
        }
    }

    protected override void Update()
    {
        base.Update();
        
        // 計算拋物線運動
        float xOffset = Mathf.Lerp(0, base.horizontalOffset, progress);
        float yOffset = Mathf.Sin(progress * Mathf.PI) * base.arcHeight;
        
        rectTransform.anchoredPosition = base.startPosition + new Vector2(xOffset, yOffset);

        // 根據生命週期調整縮放
        float scale = scaleCurve.Evaluate(progress);
        rectTransform.localScale = Vector3.one * scale;
    }
} 