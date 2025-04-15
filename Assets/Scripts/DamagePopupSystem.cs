using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DamagePopupSystem : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Vector2 position;
        public bool isActive = true;
    }

    [Header("Prefabs")]
    public GameObject floatingTextPrefab;
    public GameObject damagePopupPrefab;
    public GameObject criticalPopupPrefab;
    public GameObject dotPopupPrefab;
    public GameObject healingPopupPrefab;

    [Header("生成點設定")]
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    [Header("效果文字設定")]
    public List<string> effectTexts = new List<string>();
    public Color effectTextColor = Color.white;
    public float effectTextSize = 24f;
    public float effectTextDuration = 2f;
    public float effectTextSpeed = 1f;

    [Header("傷害設定")]
    public int minDamage = 10;
    public int maxDamage = 100;
    public Color damageTextColor = Color.red;
    public float damageTextSize = 32f;
    public float damageTextDuration = 1.5f;
    public float damageTextSpeed = 2f;
    public float damageTextArcHeight = 2f;
    public float damageTextHorizontalOffset = 2f;
    public float damageTextHorizontalOffsetMax = 2f;
    public float damageTextHorizontalOffsetMin = 2f;



    [Header("爆擊設定")]
    public int minCriticalDamage = 100;
    public int maxCriticalDamage = 500;
    public Color criticalTextColor = Color.yellow;
    public float criticalTextSize = 48f;
    public float criticalTextDuration = 2f;
    public float criticalTextSpeed = 2.5f;
    public float criticalTextArcHeight = 3f;
    public float criticalScaleMultiplier = 1.5f;
    public Sprite criticalBackground;
    public float criticalHorizontalOffset = 2f;
    public float criticalHorizontalOffsetMax = 2f;
    public float criticalHorizontalOffsetMin = 2f;


    [Header("持續傷害設定")]
    public int minDotDamage = 5;
    public int maxDotDamage = 20;
    public Color dotTextColor = Color.magenta;
    public float dotTextSize = 28f;
    public float dotTextDuration = 1.5f;
    public float dotTextSpeed = 1.5f;
    public float dotScaleMultiplier = 1.3f;

    [Header("治療設定")]
    public int minHealing = 20;
    public int maxHealing = 100;
    public Color healingTextColor = Color.green;
    public float healingTextSize = 32f;
    public float healingTextDuration = 1.5f;
    public float healingTextSpeed = 1.5f;
    public Sprite healingBackground;

    private Canvas canvas;

    private void Awake()
    {
        // 嘗試獲取父物件的 Canvas
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            // 如果找不到，嘗試在場景中查找
            canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("找不到 Canvas！請確保場景中有一個 Canvas 物件。");
                return;
            }
        }

        // 初始化生成點
        if (spawnPoints.Count == 0)
        {
            spawnPoints.Add(new SpawnPoint { position = new Vector2(-2, 0) });
            spawnPoints.Add(new SpawnPoint { position = new Vector2(2, 0) });
            spawnPoints.Add(new SpawnPoint { position = new Vector2(0, 2) });
            spawnPoints.Add(new SpawnPoint { position = new Vector2(0, -2) });
        }
    }

    public void SpawnEffect()
    {
        if (canvas == null || effectTexts.Count == 0) return;
        string randomText = effectTexts[Random.Range(0, effectTexts.Count)];
        Vector2 spawnPosition = GetRandomSpawnPoint();
        
        GameObject popupObj = Instantiate(floatingTextPrefab, canvas.transform);
        popupObj.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        
        FloatingText popup = popupObj.GetComponent<FloatingText>();
        popup.Initialize(randomText, effectTextColor, effectTextSize, effectTextDuration, effectTextSpeed);
    }

    public void SpawnDamage()
    {
        if (canvas == null) return;
        int damage = Random.Range(minDamage, maxDamage + 1);
        float dtho = Random.Range(damageTextHorizontalOffsetMin, damageTextHorizontalOffsetMax);
        Vector2 spawnPosition = GetRandomSpawnPoint();
        
        GameObject popupObj = Instantiate(damagePopupPrefab, canvas.transform);
        popupObj.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        
        DamagePopup popup = popupObj.GetComponent<DamagePopup>();
        popup.InitializeDamage(damage, damageTextColor, damageTextSize, damageTextDuration, damageTextSpeed, damageTextArcHeight, dtho);
    }

    public void SpawnCritical()
    {
        if (canvas == null) return;
        int damage = Random.Range(minCriticalDamage, maxCriticalDamage + 1);
        float ctho = Random.Range(criticalHorizontalOffsetMin, criticalHorizontalOffsetMax);
        Vector2 spawnPosition = GetRandomSpawnPoint();
        
        GameObject popupObj = Instantiate(criticalPopupPrefab, canvas.transform);
        popupObj.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        
        CriticalPopup popup = popupObj.GetComponent<CriticalPopup>();
        popup.InitializeCritical(damage, criticalTextColor, criticalTextSize, criticalTextDuration, 
            criticalTextSpeed, criticalTextArcHeight, criticalScaleMultiplier, criticalBackground, ctho);
    }

    public void SpawnDot()
    {
        if (canvas == null) return;
        int damage = Random.Range(minDotDamage, maxDotDamage + 1);
        Vector2 spawnPosition = GetRandomSpawnPoint();
        
        GameObject popupObj = Instantiate(dotPopupPrefab, canvas.transform);
        popupObj.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        
        DotPopup popup = popupObj.GetComponent<DotPopup>();
        popup.InitializeDot(damage, dotTextColor, dotTextSize, dotTextDuration, dotTextSpeed, dotScaleMultiplier);
    }

    public void SpawnHealing()
    {
        if (canvas == null) return;
        int healing = Random.Range(minHealing, maxHealing + 1);
        Vector2 spawnPosition = GetRandomSpawnPoint();
        
        GameObject popupObj = Instantiate(healingPopupPrefab, canvas.transform);
        popupObj.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        
        HealingPopup popup = popupObj.GetComponent<HealingPopup>();
        popup.InitializeHealing(healing, healingTextColor, healingTextSize, healingTextDuration, healingTextSpeed, healingBackground);
    }

    public void SpawnCombined()
    {
        if (canvas == null || effectTexts.Count == 0) return;
        
        // 獲取同一個生成點
        Vector2 spawnPosition = GetRandomSpawnPoint();

        float dtho = Random.Range(damageTextHorizontalOffsetMin, damageTextHorizontalOffsetMax);
        
        // 生成效果文字
        string randomText = effectTexts[Random.Range(0, effectTexts.Count)];
        GameObject effectObj = Instantiate(floatingTextPrefab, canvas.transform);
        effectObj.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        FloatingText effectPopup = effectObj.GetComponent<FloatingText>();
        effectPopup.Initialize(randomText, effectTextColor, effectTextSize, effectTextDuration, effectTextSpeed);
        
        // 生成傷害數字
        int damage = Random.Range(minDamage, maxDamage + 1);
        GameObject damageObj = Instantiate(damagePopupPrefab, canvas.transform);
        damageObj.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        DamagePopup damagePopup = damageObj.GetComponent<DamagePopup>();
        damagePopup.InitializeDamage(damage, damageTextColor, damageTextSize, damageTextDuration, damageTextSpeed, damageTextArcHeight, dtho);
    }

    private Vector2 GetRandomSpawnPoint()
    {
        List<SpawnPoint> activePoints = spawnPoints.FindAll(p => p.isActive);
        if (activePoints.Count == 0) return Vector2.zero;
        return activePoints[Random.Range(0, activePoints.Count)].position;
    }
} 