using UnityEngine;

public class CharacterUnlockUI : MonoBehaviour
{
    [SerializeField] private int _characterId; // 1, 2, or 3
    [SerializeField] private GameObject _lockedPanel;
    [SerializeField] private GameObject _unlockedPanel;

    void Start()
    {
        if (_characterId == 1)
        {
            ShowUnlocked();
            return;
        }

        DBManager.Instance.CheckCharacterUnlock(_characterId, (unlocked) => {
            if (unlocked)
                ShowUnlocked();
            else
                ShowLocked();
        });
    }

    private void ShowUnlocked()
    {
        _lockedPanel.SetActive(false);
        _unlockedPanel.SetActive(true);
    }

    private void ShowLocked()
    {
        _lockedPanel.SetActive(true);
        _unlockedPanel.SetActive(false);
    }
}