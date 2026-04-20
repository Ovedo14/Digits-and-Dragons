using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _eventImage;
    
    [Header("Option Buttons")]
    [SerializeField] private GameObject _optionButtonPrefab;
    [SerializeField] private Transform _optionsContainer;

    private GameEvent _currentEvent;

    public void ShowEvent(GameEvent gameEvent)
    {
        _currentEvent = gameEvent;

        _titleText.text = gameEvent.Title;
        _descriptionText.text = gameEvent.Description;
        _eventImage.sprite = gameEvent.EventImage;

        foreach (Transform child in _optionsContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < gameEvent.Options.Count; i++)
        {
            int index = i; // Capture for lambda
            GameObject buttonObj = Instantiate(_optionButtonPrefab, _optionsContainer);
            
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            
            buttonText.text = gameEvent.Options[index].OptionText;
            button.onClick.AddListener(() => OnOptionSelected(index));
        }
    }

    private void OnOptionSelected(int optionIndex)
    {
        _currentEvent.Options[optionIndex].Effect.Apply();
        EventBus.Publish(new OnEventCompleted());
        EventBus.Publish(new OnTurnStarted());
    }
}