using UnityEngine;
using TMPro;

public class EnemyHPUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private EnemyManager _enemyManager;

    void OnEnable()
    {
        EventBus.Subscribe<OnDamageDealt>(HandleDamageDealt);
        EventBus.Subscribe<OnCombatCompleted>(HandleCombatCompleted);
        EventBus.Subscribe<OnTurnStarted>(HandleTurnStarted);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnDamageDealt>(HandleDamageDealt);
        EventBus.Unsubscribe<OnCombatCompleted>(HandleCombatCompleted);
        EventBus.Unsubscribe<OnTurnStarted>(HandleTurnStarted);
    }

    void Start()
    {
        RefreshHP();
    }

    private void HandleDamageDealt(OnDamageDealt evt)
    {
        if (evt.ToPlayer) return;
        RefreshHP();
    }

    private void HandleCombatCompleted(OnCombatCompleted evt)
    {
        RefreshHP();
    }

    private void RefreshHP()
    {
        float currentHP = _enemyManager.GetCurrentHP();
        float maxHP = _enemyManager.GetCurrentEnemy().MaxHP;
        _hpText.text = currentHP + " / " + maxHP;
    }

    private void HandleTurnStarted(OnTurnStarted evt)
    {
        Debug.Log("OnTurnStarted received in EnemyHPUI");
        RefreshHP();
    }
}