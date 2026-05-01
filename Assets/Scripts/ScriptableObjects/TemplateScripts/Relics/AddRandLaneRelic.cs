using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Add Random Lane Relic")]
public class LaneBuffRelic : RelicData
{
    public float bonusValue;
    public float bonusHP;
    public override void OnAcquire()
    {
        RunManager.Instance.PlayerMaxHP += bonusHP;
        RunManager.Instance.PlayerHP += bonusHP;
    }


    public override void ApplyEffect(RelicContext context)
    {
        int laneIndex = Random.Range(0, 3);

        LaneManager.Instance.AddPlayerValue(laneIndex, bonusValue);
    }
}