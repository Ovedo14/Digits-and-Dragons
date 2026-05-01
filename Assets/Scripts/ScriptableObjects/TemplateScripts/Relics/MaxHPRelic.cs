using UnityEngine;

[CreateAssetMenu(menuName = "Relics/MaxHP Relic")]
public class MaxHpRelic : RelicData
{
    public float bonusHP;

    public override void OnAcquire()
    {
        RunManager.Instance.PlayerMaxHP += bonusHP;
        RunManager.Instance.PlayerHP += bonusHP;
    }
    
    public override void ApplyEffect(RelicContext context)
    {
    }
}
