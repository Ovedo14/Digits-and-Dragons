using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Spread Value Relic")]
public class SpreadValueRelic : RelicData
{
    public float percentBoost;
    public float extraHp;

    public override void OnAcquire()
    {
        RunManager.Instance.PlayerMaxHP += extraHp;
        RunManager.Instance.PlayerHP += extraHp;
    }


    public override void ApplyEffect(RelicContext context)
    {
        LaneManager.Instance.AddPlayerValue(1, context.LastCardPlayed.Value * percentBoost);
        LaneManager.Instance.AddPlayerValue(2, context.LastCardPlayed.Value * percentBoost);
        LaneManager.Instance.AddPlayerValue(3, context.LastCardPlayed.Value * percentBoost);
    }
}
