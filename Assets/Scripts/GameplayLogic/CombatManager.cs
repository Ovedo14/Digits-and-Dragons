using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private bool _combatActive = false;

    void OnEnable()
    {
        EventBus.Subscribe<OnLanesResolved>(HandleLanesResolved);
        EventBus.Subscribe<OnCombatEnded>(HandleCombatEnded);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnLanesResolved>(HandleLanesResolved);
        EventBus.Unsubscribe<OnCombatEnded>(HandleCombatEnded);
    }

    public void StartCombat()
    {
        _combatActive = true;
        StartTurn();
    }

    private void StartTurn()
    {
        if (!_combatActive) return;
        Debug.Log("Publishing OnTurnStarted");
        EventBus.Publish(new OnTurnStarted());
        
    }

    public void EndTurn()
    {
        if (!_combatActive) return;
        EventBus.Publish(new OnTurnEnded());
    }

    private void HandleLanesResolved(OnLanesResolved evt)
    {
        //Just checks if combat isnt over since LaneManager handles damage
        if (_combatActive)
            StartTurn();
    }

    private void HandleCombatEnded(OnCombatEnded evt)
    {
        _combatActive = false;
        
        if (!evt.PlayerWon)
        {
            EventBus.Publish(new OnPlayerLost());
        }
        else if (RunManager.Instance.IsFinalBoss)
        {
            EventBus.Publish(new OnPlayerWon());
        }
        else
        {
            EventBus.Publish(new OnCombatCompleted());
        }
    }
}