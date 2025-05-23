using UnityEngine;
using DG.Tweening;

public class DamagePopup : FloatingText
{
    protected float arcHeight;
    protected Vector2 startPosition;
    protected float horizontalOffset = 2f; // 水平偏移量

    public void InitializeDamage(int damage, Color color, float size, float duration, float speed, float arcHeight, float horizontalOffset)
    {
        base.Initialize(damage.ToString(), color, size, duration, speed);
        this.arcHeight = arcHeight;
        this.startPosition = rectTransform.anchoredPosition;
        this.horizontalOffset = horizontalOffset * 0.5f + startPosition.x;
    }

    protected override void Update()
    {
        base.Update();
        // 計算拋物線運動
        float xOffset = Mathf.Lerp(0, horizontalOffset, progress);
        float yOffset = Mathf.Sin(progress * Mathf.PI) * arcHeight;
        
        rectTransform.anchoredPosition = startPosition + new Vector2(xOffset, yOffset);
    }
} 