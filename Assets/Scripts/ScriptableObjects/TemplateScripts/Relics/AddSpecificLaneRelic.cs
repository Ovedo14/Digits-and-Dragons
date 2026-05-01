using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Add Specific Lane Relic")]
public class AddSpecificLaneRelic : RelicData
{
    public float bonusValue1 = 0;
    public float bonusValue2 = 0;
    public float bonusValue3 = 0;

    public override void ApplyEffect(RelicContext context)
    {
        LaneManager.Instance.AddPlayerValue(1, bonusValue1);
        LaneManager.Instance.AddPlayerValue(2, bonusValue2);
        LaneManager.Instance.AddPlayerValue(3, bonusValue3);
    }
}