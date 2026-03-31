using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] public GameObject settingsPanel;

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}