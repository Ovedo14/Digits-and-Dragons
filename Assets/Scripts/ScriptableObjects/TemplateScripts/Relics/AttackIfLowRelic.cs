using UnityEngine;
[CreateAssetMenu(menuName = "Relics/AttakcIfLow Relic")]

public class HealIfRelic : RelicData
{
    public float bonusValue1 = 0;
    public float bonusValue2 = 0;
    public float bonusValue3 = 0;
    public float extraHp;

    public override void OnAcquire()
    {
        RunManager.Instance.PlayerMaxHP += extraHp;
        RunManager.Instance.PlayerHP += extraHp;
    }

    public override void ApplyEffect(RelicContext context)
    {
        if (RunManager.Instance.PlayerHP <= RunManager.Instance.PlayerMaxHP * 0.3)
        {
            LaneManager.Instance.AddPlayerValue(1, bonusValue1);
            LaneManager.Instance.AddPlayerValue(2, bonusValue2);
            LaneManager.Instance.AddPlayerValue(3, bonusValue3);
        } 
    }
}