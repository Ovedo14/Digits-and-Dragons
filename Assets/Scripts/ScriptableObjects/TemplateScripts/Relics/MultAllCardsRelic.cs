using UnityEngine;

[CreateAssetMenu(menuName = "Relics/MultAllCard")]
public class MultAllCardsRelic : RelicData
{
    [SerializeField] private float _bonusAmount;
    public float bonusHP;

    public override void OnAcquire()
    {
        RunManager.Instance.PlayerMaxHP += bonusHP;
        RunManager.Instance.PlayerHP += bonusHP;
    }

    public override void ApplyEffect(RelicContext context)
    {
        context.LastCardPlayed.Value *= _bonusAmount;
    }
}