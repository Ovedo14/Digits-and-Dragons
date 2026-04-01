using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Visuals")]
    public Sprite EnemySprite;

    [Header("Animations")]
    public AnimationClip IdleAnimation;
    public AnimationClip AttackAnimation;

    [Header("Stats")]
    public int MaxHP;

    [Header("Behavior")]
    public LaneBias Bias;
}