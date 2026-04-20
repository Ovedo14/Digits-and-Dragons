using UnityEngine;

[CreateAssetMenu(menuName = "Game/Events/Effects/Damage")]
public class DamageEffect : EventEffect
{
    [SerializeField] private float _damageAmount;

    public override void Apply()
    {
        RunManager.Instance.ApplyDamage(_damageAmount);
    }
}