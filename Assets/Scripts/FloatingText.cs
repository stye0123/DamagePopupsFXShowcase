using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class FloatingText : MonoBehaviour
{
    protected TextMeshProUGUI textComponent;
    protected Image backgroundImage;
    protected RectTransform rectTransform;
    protected float duration;
    protected float speed;
    protected float scaleMultiplier = 1f;
    protected bool useBackground = false;
    protected float progress = 0f;
    protected float lifeTime = 0f;

    protected virtual void Awake()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        backgroundImage = GetComponentInChildren<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void Initialize(string text, Color color, float size, float duration, float speed, 
        float scaleMultiplier = 1f, Sprite background = null)
    {
        this.duration = duration;
        this.speed = speed;
        this.scaleMultiplier = scaleMultiplier;
        this.lifeTime = duration;

        if (textComponent != null)
        {
            textComponent.text = text;
            textComponent.color = color;
            textComponent.fontSize = size;
        }

        if (backgroundImage != null && background != null)
        {
            backgroundImage.sprite = background;
            backgroundImage.enabled = true;
            useBackground = true;
        }
        else if (backgroundImage != null)
        {
            backgroundImage.enabled = false;
        }

        // 初始縮放
        rectTransform.localScale = Vector3.one * scaleMultiplier;
        rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

        // 淡出效果
        if (textComponent != null)
        {
            textComponent.DOFade(0f, duration).SetEase(Ease.InQuad);
        }
        if (backgroundImage != null)
        {
            backgroundImage.DOFade(0f, duration).SetEase(Ease.InQuad);
        }

        // 銷毀物件
        Destroy(gameObject, duration);
    }

    protected virtual void Update()
    {
        // 更新進度
        progress += Time.deltaTime / lifeTime;
        
        // 基本向上移動
        rectTransform.anchoredPosition += Vector2.up * speed * Time.deltaTime;
    }
} 