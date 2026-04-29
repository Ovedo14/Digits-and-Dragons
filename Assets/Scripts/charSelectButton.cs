using UnityEngine;
using UnityEngine.SceneManagement;

public class charSelectButton : MonoBehaviour
{
    [SerializeField] private CharacterData _character;
    public void OnSelectCharacterPressed()
    {
        DBManager.Instance.StartNewRun(_character.characterId, _character.StartingHP, (runId) => {

            if (runId <= 0)
            {
                Debug.LogWarning("Failed to get runId from backend. Using fallback.");
                runId = -1; // fallback value
            }

            RunManager.Instance.RunId = runId;
            RunManager.Instance.CharacterId = _character.characterId;
            RunManager.Instance.InitializeRun(_character);

            SceneManager.LoadScene("Gameplay");
        });
    }
}