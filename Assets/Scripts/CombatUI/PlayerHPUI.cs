using UnityEngine;
using TMPro;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;

    void OnEnable()
    {
        EventBus.Subscribe<OnDamageDealt>(HandleDamageDealt);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnDamageDealt>(HandleDamageDealt);
    }

    void Start()
    {
        RefreshHP();
    }

    private void HandleDamageDealt(OnDamageDealt evt)
    {
        if (!evt.ToPlayer) return;
        RefreshHP();
    }

    private void RefreshHP()
    {
        _hpText.text = RunManager.Instance.PlayerHP + " / " + RunManager.Instance.PlayerMaxHP;
    }
}