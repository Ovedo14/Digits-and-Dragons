using UnityEngine;

[CreateAssetMenu(menuName = "Game/Events/Effects/AddRelic")]
public class AddRelicEffect : EventEffect
{
    [SerializeField] private RelicData _relic;

    public override void Apply()
    {
        RunManager.Instance.Relics.Add(_relic);
    }
}