using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Image _artwork;
    [SerializeField] private TextMeshProUGUI _operationText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Button _button;

    private int _handIndex;
    private CardHandUI _handUI;

    public void Setup(CardData card, int handIndex, CardHandUI handUI)
    {
        _handIndex = handIndex;
        _handUI = handUI;

        _artwork.sprite = card.Artwork;
        _valueText.text = card.Value.ToString("0.#");
        _operationText.text = card.Operation switch
        {
            CardOperation.Add      => "+",
            CardOperation.Multiply => "×",
            _                      => "?"
        };

        _button.onClick.AddListener(OnCardClicked);
    }

    private void OnCardClicked()
    {
        _handUI.OnCardSelected(_handIndex);
    }

    void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}