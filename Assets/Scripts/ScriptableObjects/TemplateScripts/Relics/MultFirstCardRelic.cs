using UnityEngine;

[CreateAssetMenu(menuName = "Relics/MultFirstCard")]
public class MultFirstCardRelic : RelicData
{
    [SerializeField] private float _bonusAmount;
    private bool _usedThisTurn = false;

    public override void OnAcquire()
    {
        EventBus.Subscribe<OnTurnStarted>(ResetUse);
    }

    private void ResetUse(OnTurnStarted evt)
    {
        _usedThisTurn = false;
    }

    public override void ApplyEffect(RelicContext context)
    {
        if (_usedThisTurn) return;

        context.LastCardPlayed.Value *= _bonusAmount;
        _usedThisTurn = true;
    }
}