using UnityEngine;

public class EndTurnButtonUI : MonoBehaviour
{
    [SerializeField] private CombatManager _combatManager;

    public void OnEndTurnPressed()
    {
        _combatManager.EndTurn();
    }
}