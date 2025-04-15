using UnityEngine;
using DG.Tweening;

public class DotPopup : FloatingText
{
    private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 1.3f, 1, 1f);

    public void InitializeDot(int damage, Color color, float size, float duration, float speed, float scaleMultiplier)
    {
        base.Initialize(damage.ToString(), color, size, duration, speed, scaleMultiplier);
        
        // 初始縮放效果
        //Vector3.one 是 Unity 的 Vector3 結構，表示一個大小為 1 的向量
        //scaleMultiplier 是傳入的參數，表示縮放倍數
        //Vector3.one * scaleMultiplier 是將 Vector3.one 乘以 scaleMultiplier，表示縮放後的大小
        //0.2f 是動畫的持續時間
        //Ease.OutBack 是動畫的緩動函數，表示動畫會從慢到快
        //.OnComplete(() => {
        //    rectTransform.DOScale(Vector3.one, 0.3f)
        rectTransform.DOScale(Vector3.one * scaleMultiplier, 0.2f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => {
                rectTransform.DOScale(Vector3.one, 0.3f)
                    .SetEase(Ease.InOutQuad);
            });//這段程式碼是當縮放動畫完成後，會執行這個匿名函式
    }

    protected override void Update()
    {
        base.Update();
        
        // 根據生命週期調整縮放
        float scale = scaleCurve.Evaluate(progress);
        //progress 是從 0 到 1 的值，表示動畫的進度
        //scaleCurve 是動畫的曲線，表示動畫的進度
        //Evaluate 是評估動畫的進度
        //Vector3.one 是 Unity 的 Vector3 結構，表示一個大小為 1 的向量
        //scale 是評估後的值
        //Vector3.one * scale 是將 Vector3.one 乘以 scale，表示縮放後的大小
        rectTransform.localScale = Vector3.one * scale;
    }
} 