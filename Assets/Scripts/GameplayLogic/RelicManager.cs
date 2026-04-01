using UnityEngine;
using System.Collections.Generic;

public class RelicManager : MonoBehaviour
{
    private List<RelicData> _activeRelics = new List<RelicData>();

    void Start()
    {
        _activeRelics = RunManager.Instance.Relics;
    }

    void OnEnable()
    {
        EventBus.Subscribe<OnTurnStarted>(HandleTurnStarted);
        EventBus.Subscribe<OnTurnEnded>(HandleTurnEnded);
        EventBus.Subscribe<OnCardPlayed>(HandleCardPlayed);
        EventBus.Subscribe<OnDamageDealt>(HandleDamageDealt);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnTurnStarted>(HandleTurnStarted);
        EventBus.Unsubscribe<OnTurnEnded>(HandleTurnEnded);
        EventBus.Unsubscribe<OnCardPlayed>(HandleCardPlayed);
        EventBus.Unsubscribe<OnDamageDealt>(HandleDamageDealt);
    }

    private void HandleTurnStarted(OnTurnStarted evt) => 
        TriggerRelics(RelicTrigger.OnTurnStart);

    private void HandleTurnEnded(OnTurnEnded evt) => 
        TriggerRelics(RelicTrigger.OnTurnEnd);

    private void HandleCardPlayed(OnCardPlayed evt) => 
        TriggerRelics(RelicTrigger.OnCardPlayed);

    private void HandleDamageDealt(OnDamageDealt evt) => 
        TriggerRelics(RelicTrigger.OnDamageDealt);

    private void TriggerRelics(RelicTrigger trigger)
    {
        foreach (RelicData relic in _activeRelics)
        {
            if (relic.Trigger != trigger) continue;

            relic.ApplyEffect();
            EventBus.Publish(new OnRelicTriggered { Relic = relic });
        }
    }

    public void AddRelic(RelicData relic)
    {
        _activeRelics.Add(relic);
        RunManager.Instance.Relics.Add(relic);
    }
}