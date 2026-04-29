using UnityEngine;

public struct Lane
{
    public float PlayerValue;
    public float EnemyValue;
}

[System.Serializable]
public class EnemyStagePool
{
    public EnemyData[] enemies;
}

[System.Serializable]
public struct AdditionSettings
{
    public int MinTerms;
    public int MaxTerms;
    public float MinValue;
    public float MaxValue;
}

[System.Serializable]
public struct MultiplicationSettings
{
    public int MinTerms;
    public int MaxTerms;
    public float MinValue;
    public float MaxValue;
}

[System.Serializable]
public struct DivisionSettings
{
    public float MinDividend;
    public float MaxDividend;
    public float MinDivisor;
    public float MaxDivisor;
}

    public class RelicContext
    {
        public CardData LastCardPlayed;
        public int LastLaneIndex;
        public float LastDamageAmount;
        
    }