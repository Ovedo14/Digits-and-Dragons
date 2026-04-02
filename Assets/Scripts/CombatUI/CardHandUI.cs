using UnityEngine;
using System.Collections.Generic;

public class CardHandUI : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private LaneManager _laneManager;

    private List<GameObject> _cardObjects = new List<GameObject>();
    private int _selectedCardIndex = -1;

    void OnEnable()
    {
        EventBus.Subscribe<OnTurnStarted>(HandleTurnStarted);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnTurnStarted>(HandleTurnStarted);
    }

    private void HandleTurnStarted(OnTurnStarted evt) => RenderHand();

    private void RenderHand()
    {
        //Clear existing cards
        foreach (GameObject card in _cardObjects)
            Destroy(card);
        _cardObjects.Clear();
        _selectedCardIndex = -1;

        //Spawn one prefab per card in hand
        List<CardData> hand = _deckManager.GetHand();
        for (int i = 0; i < hand.Count; i++)
        {
            GameObject cardObject = Instantiate(_cardPrefab, transform);
            cardObject.GetComponent<CardUI>().Setup(hand[i], i, this);
            _cardObjects.Add(cardObject);
        }
    }

    public void OnCardSelected(int handIndex)
    {
        _selectedCardIndex = handIndex;
        HighlightCard(handIndex);
    }

    public void OnLaneSelected(int laneIndex)
    {
        if (_selectedCardIndex == -1) return;

        _deckManager.PlayCard(_selectedCardIndex, laneIndex);
        _selectedCardIndex = -1;
        RenderHand();
    }

    private void HighlightCard(int handIndex)
    {
        for (int i = 0; i < _cardObjects.Count; i++)
        {
            CanvasGroup group = _cardObjects[i].GetComponent<CanvasGroup>();
            if (group != null)
                group.alpha = i == handIndex ? 1f : 0.5f;
        }
    }
}