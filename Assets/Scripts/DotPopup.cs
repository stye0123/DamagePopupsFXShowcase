using UnityEngine;
using DG.Tweening;

public class DotPopup : FloatingText
{
    private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 1.3f, 1, 1f);

    public void InitializeDot(int damage, Color color, float size, float duration, float speed, float scaleMultiplier)
    {
        base.Initialize(damage.ToString(), color, size, duration, speed, scaleMultiplier);
        
        // 初始縮放效果
        rectTransform.DOScale(Vector3.one * scaleMultiplier, 0.2f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => {
                rectTransform.DOScale(Vector3.one, 0.3f)
                    .SetEase(Ease.InOutQuad);
            });
    }

    protected override void Update()
    {
        base.Update();
        
        // 根據生命週期調整縮放
        float scale = scaleCurve.Evaluate(progress);
        rectTransform.localScale = Vector3.one * scale;
    }
} 