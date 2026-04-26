using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    [Header("Hand Settings")]
    [SerializeField] private int _handSize = 5;
    [SerializeField] private int _guaranteedAdditions = 3;

    [Header("References")]
    [SerializeField] private EnemyManager _enemyManager;

    [Header("Balance Scaling")]
    [SerializeField] private float _additionPercentOfThreat = 0.7f; //each add ~n% of avg threat
    [SerializeField] private float _multiplyMin = 1.5f;
    [SerializeField] private float _multiplyMax = 3f;

    [SerializeField] private List<CardData> _cardPool;
    private List<CardData> _hand = new List<CardData>();

    void OnEnable() => EventBus.Subscribe<OnEnemyEquationsGenerated>(HandleEnemyEquationsGenerated);
    void OnDisable() => EventBus.Unsubscribe<OnEnemyEquationsGenerated>(HandleEnemyEquationsGenerated);

    private void HandleEnemyEquationsGenerated(OnEnemyEquationsGenerated evt) => GenerateHand();

    private void GenerateHand()
    {
        _hand.Clear();

        float averageThreat = CalculateAverageThreat();

        //guarantee drawing additions first
        for (int i = 0; i < _guaranteedAdditions; i++)
        {
            CardData card = GenerateContextualCard(CardOperation.Add, averageThreat);
            _hand.Add(card);
        }

        //fill rest randomly
        for (int i = _guaranteedAdditions; i < _handSize; i++)
        {
            CardOperation operation = Random.value > 0.5f ? CardOperation.Add : CardOperation.Multiply;
            CardData card = GenerateContextualCard(operation, averageThreat);
            _hand.Add(card);
        }
    }

    private float CalculateAverageThreat()
    {
        float total = 0;
        for (int i = 0; i < 3; i++)
        {
            LaneEquation eq = _enemyManager.GetLaneEquation(i);
            total += CalculateEquationValue(eq);
        }
        return total / 3f;
    }

    private float CalculateEquationValue(LaneEquation equation)
    {
        switch (equation.OpType)
        {
            case OperationType.Add:
                float sum = 0;
                foreach (float term in equation.Terms)
                    sum += term;
                return sum;

            case OperationType.Mult:
                float product = 1;
                foreach (float term in equation.Terms)
                    product *= term;
                return product;

            case OperationType.Div:
                return equation.Terms[0] / equation.Terms[1];

            default:
                return 0;
        }
    }

    private CardData GenerateContextualCard(CardOperation operation, float averageThreat)
    {
        CardData template = _cardPool.Find(c => c.Operation == operation);
        if (template == null) template = _cardPool[0];

        CardData card = ScriptableObject.CreateInstance<CardData>();
        card.Operation = operation;
        card.Artwork = template.Artwork;

        switch (operation)
        {
            case CardOperation.Add:
                //balances depending on enemy attack
                float addMin = averageThreat * _additionPercentOfThreat * 0.7f;
                float addMax = averageThreat * _additionPercentOfThreat * 1.3f;
                card.Value = Mathf.Max(1, Mathf.Round(Random.Range(addMin, addMax)));
                break;

            case CardOperation.Multiply:
                card.Value = Mathf.Round(Random.Range(_multiplyMin, _multiplyMax) * 10f) / 10f;
                break;
        }

        return card;
    }

    public void PlayCard(int handIndex, int laneIndex)
    {
        if (handIndex < 0 || handIndex >= _hand.Count) return;

        CardData card = _hand[handIndex];
        _hand.RemoveAt(handIndex);

        EventBus.Publish(new OnCardPlayed { Card = card, LaneIndex = laneIndex });
    }

    public List<CardData> GetHand() => _hand;
}