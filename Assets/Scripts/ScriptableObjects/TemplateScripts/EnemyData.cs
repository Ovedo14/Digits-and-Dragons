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

    [Header("Operation")]
    public OperationType Operation;

    [Header("Equation Balance")]
    public AdditionSettings AddSettings = new AdditionSettings 
    { 
        MinTerms = 2, MaxTerms = 4, MinValue = 1, MaxValue = 10 
    };
    
    public MultiplicationSettings MultSettings = new MultiplicationSettings 
    { 
        MinTerms = 2, MaxTerms = 3, MinValue = 2, MaxValue = 4 
    };
    
    public DivisionSettings DivSettings = new DivisionSettings 
    { 
        MinDividend = 10, MaxDividend = 30, MinDivisor = 2, MaxDivisor = 5 
    };

    [Header("Rewards")]
    public int GoldReward = 10;
    public float HealthReward = 5;
    public RelicData RelicDrop;
}