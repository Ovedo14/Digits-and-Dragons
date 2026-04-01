using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct CardGenerationSettings  //min and max values are unique for card types for balance
{
    public CardOperation Operation;
    public float MinValue;
    public float MaxValue;
}

public class DeckManager : MonoBehaviour
{
    [Header("Hand Settings")]
    [SerializeField] private int _handSize = 5;

    [Header("Card Generation")]
    [SerializeField] private List<CardGenerationSettings> _operationSettings = new List<CardGenerationSettings>
    {
        new CardGenerationSettings { Operation = CardOperation.Add,      MinValue = 1,   MaxValue = 20  },
        new CardGenerationSettings { Operation = CardOperation.Multiply, MinValue = 1,   MaxValue = 3   }
    };

    [SerializeField] private List<CardData> _cardPool; //List of cards to generate from (only card type and sprite, can be changed by enemy)
    private List<CardData> _hand = new List<CardData>();

    void OnEnable()
    {
        EventBus.Subscribe<OnTurnStarted>(HandleTurnStarted);
    }
    void OnDisable() 
    { 
        EventBus.Unsubscribe<OnTurnStarted>(HandleTurnStarted);
    }

    private void HandleTurnStarted(OnTurnStarted evt)
    {
        GenerateHand();  
    } 

    private void GenerateHand()
    {
        _hand.Clear();

        for (int i = 0; i < _handSize; i++)
        {
            CardData template = _cardPool[Random.Range(0, _cardPool.Count)];
            CardGenerationSettings settings = GetSettingsFor(template.Operation);

            CardData card = ScriptableObject.CreateInstance<CardData>();
            card.Operation = template.Operation;
            card.Artwork = template.Artwork;
            card.Value = Mathf.Round(Random.Range(settings.MinValue, settings.MaxValue) * 10f) / 10f; //un decimal maximo

            _hand.Add(card);
        }
    }

    private CardGenerationSettings GetSettingsFor(CardOperation operation)
    {
        foreach (var setting in _operationSettings)
            if (setting.Operation == operation) return setting;

        //default para que no se mate todo si no encuentra un setting
        return new CardGenerationSettings { Operation = operation, MinValue = 1, MaxValue = 2 };
    }

    public void PlayCard(int handIndex, int laneIndex)
    {
        if (handIndex < 0 || handIndex >= _hand.Count) return; //default para que no se mate todo por alguna razon

        CardData card = _hand[handIndex];
        _hand.RemoveAt(handIndex);

        EventBus.Publish(new OnCardPlayed { Card = card, LaneIndex = laneIndex });
    }

    public List<CardData> GetHand() => _hand;
}