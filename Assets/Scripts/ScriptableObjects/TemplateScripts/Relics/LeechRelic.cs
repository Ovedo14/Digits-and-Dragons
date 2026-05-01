using UnityEngine;


[CreateAssetMenu(menuName = "Relics/Leech Relic")]

public class LeechRelic : RelicData
{
    [SerializeField] private float percentHeal;
    public float bonusHP;
    public override void OnAcquire()
    {
        RunManager.Instance.PlayerMaxHP += bonusHP;
        RunManager.Instance.PlayerHP += bonusHP;
    }

    public override void ApplyEffect(RelicContext context)
    {
        
        RunManager.Instance.HealPlayer(context.LastDamageAmount * percentHeal);
    }
}