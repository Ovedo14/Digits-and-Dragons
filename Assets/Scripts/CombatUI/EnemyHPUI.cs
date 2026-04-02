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
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnDamageDealt>(HandleDamageDealt);
        EventBus.Unsubscribe<OnCombatCompleted>(HandleCombatCompleted);
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
        _hpText.text = _enemyManager.GetCurrentHP() + " / " + _enemyManager.GetCurrentEnemy().MaxHP;
    }
}