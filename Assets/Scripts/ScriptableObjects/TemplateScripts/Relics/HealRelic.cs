using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Heal Relic")]
public class HealRelic : RelicData
{
    public float healAmount;
    public float extraHp;

    public override void OnAcquire()
    {
        RunManager.Instance.PlayerMaxHP += extraHp;
        RunManager.Instance.PlayerHP += extraHp;
    }

    public override void ApplyEffect(RelicContext context)
    {
        RunManager.Instance.HealPlayer(healAmount);
    }
}