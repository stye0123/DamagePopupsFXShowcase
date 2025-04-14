using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [Header("按鈕引用")]
    public Button effectButton;
    public Button damageButton;
    public Button criticalButton;
    public Button dotButton;
    public Button healingButton;
    public Button combineButton;

    private DamagePopupSystem popupSystem;

    private void Start()
    {
        popupSystem = GetComponent<DamagePopupSystem>();
        if (popupSystem == null)
        {
            Debug.LogError("找不到 DamagePopupSystem 組件！");
            return;
        }

        // 綁定按鈕事件
        if (effectButton != null)
            effectButton.onClick.AddListener(OnEffectButtonClicked);
        if (damageButton != null)
            damageButton.onClick.AddListener(OnDamageButtonClicked);
        if (criticalButton != null)
            criticalButton.onClick.AddListener(OnCriticalButtonClicked);
        if (dotButton != null)
            dotButton.onClick.AddListener(OnDotButtonClicked);
        if (healingButton != null)
            healingButton.onClick.AddListener(OnHealingButtonClicked);
        if (combineButton != null)
            combineButton.onClick.AddListener(OnCombineButtonClicked);
    }

    private void OnEffectButtonClicked()
    {
        popupSystem.SpawnEffect();
    }

    private void OnDamageButtonClicked()
    {
        popupSystem.SpawnDamage();
    }

    private void OnCriticalButtonClicked()
    {
        popupSystem.SpawnCritical();
    }

    private void OnDotButtonClicked()
    {
        popupSystem.SpawnDot();
    }

    private void OnHealingButtonClicked()
    {
        popupSystem.SpawnHealing();
    }

    private void OnCombineButtonClicked()
    {
        popupSystem.SpawnCombined();
    }
} 