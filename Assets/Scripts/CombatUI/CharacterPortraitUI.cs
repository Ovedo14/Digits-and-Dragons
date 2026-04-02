using UnityEngine;
using UnityEngine.UI;

public class CharacterPortraitUI : MonoBehaviour
{
    [SerializeField] private Image _portraitImage;

    void Start()
    {
        _portraitImage.sprite = RunManager.Instance.CurrentCharacter.Portrait;
    }
}