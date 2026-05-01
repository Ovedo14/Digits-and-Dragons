using UnityEngine;

[CreateAssetMenu(menuName = "Relics/ExtraDmgCard")]
public class ExtraDmgRelic : RelicData
{
    [SerializeField] private int _extraDamage;


    public override void ApplyEffect(RelicContext context)
    {
        EnemyManager.Instance._currentHP -= _extraDamage;
        EnemyManager.Instance.HandleExtraDmg();
    }
}