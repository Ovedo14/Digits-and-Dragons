using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Heal Relic")]
public class HealRelicData : RelicData
{
    public float healAmount;

    public override void ApplyEffect(RelicContext context)
    {
        RunManager.Instance.HealPlayer(healAmount);
    }
}