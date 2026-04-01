using UnityEngine;


[CreateAssetMenu(menuName = "Game/Card")]
public class CardData : ScriptableObject
{
    public CardOperation Operation;
    public float Value;
    public Sprite Artwork;

    public float ApplyTo(float laneValue)
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