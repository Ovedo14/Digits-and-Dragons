using UnityEngine;

[CreateAssetMenu(menuName = "Relics/ExtraDraw")]
public class ExtraDrawRelic : RelicData
{
    public override void OnAcquire()
    {
        DeckManager.Instance._handSize += 1;
    }
    public override void ApplyEffect(RelicContext context)
    {
        
    }
}