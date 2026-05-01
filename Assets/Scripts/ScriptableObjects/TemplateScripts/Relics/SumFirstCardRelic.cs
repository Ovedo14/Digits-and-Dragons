using UnityEngine;

[CreateAssetMenu(menuName = "Relics/AddFirstCard")]
public class SumFirstCardRelic : RelicData
{
    [SerializeField] private float _bonusAmount;
    public float bonusHP;
    private bool _usedThisTurn = false;

    public override void OnAcquire()
    {
        EventBus.Subscribe<OnTurnStarted>(ResetUse);
        RunManager.Instance.PlayerMaxHP += bonusHP;
        RunManager.Instance.PlayerHP += bonusHP;
    }

    private void ResetUse(OnTurnStarted evt)
    {
        _usedThisTurn = false;
    }

    public override void ApplyEffect(RelicContext context)
    {
        if (_usedThisTurn) return;

        if (context.LastCardPlayed.Operation == CardOperation.Add) 
        {
        context.LastCardPlayed.Value += _bonusAmount;
        _usedThisTurn = true;
        } else return;
    }
}