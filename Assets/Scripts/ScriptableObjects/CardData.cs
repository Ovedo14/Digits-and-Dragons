using UnityEngine;

public enum CardOperation {Add, Subtract, Multiply}

[CreateAssetMenu(menuName = "Game/Card")]
public class CardData : ScriptableObject
{
    public CardOperation Operation;
    public int Value;
    public Sprite Artwork;

    public int ApplyTo(int laneValue)
    {
        return Operation switch
        {
            CardOperation.Add      => laneValue + Value,
            CardOperation.Subtract => laneValue - Value,
            CardOperation.Multiply => laneValue * Value,
            _ => laneValue
        };
    }
}