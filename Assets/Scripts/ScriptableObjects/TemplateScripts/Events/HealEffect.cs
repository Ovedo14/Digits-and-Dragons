using UnityEngine;

[CreateAssetMenu(menuName = "Game/Events/Effects/Heal")]
public class HealEffect : EventEffect
{
    [SerializeField] private float _healAmount;

    public override void Apply()
    {
        RunManager.Instance.HealPlayer(_healAmount);
    }
}