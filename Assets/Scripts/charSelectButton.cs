using UnityEngine;
using UnityEngine.SceneManagement;

public class charSelectButton : MonoBehaviour
{
    [SerializeField] private CharacterData _character;

    public void OnSelectCharacterPressed()
    {
        RunManager.Instance.InitializeRun(_character);
        SceneManager.LoadScene("Gameplay");
    }
}