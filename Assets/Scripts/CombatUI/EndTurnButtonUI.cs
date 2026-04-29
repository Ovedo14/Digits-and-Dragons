using UnityEngine;

public class EndTurnButtonUI : MonoBehaviour
{
    [SerializeField] private CombatManager _combatManager;

    public void OnEndTurnPressed()
    {
        AudioManager.Instance.PlayTurnEndClash();
        _combatManager.EndTurn();
    }
}