using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LaneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _playerValueTexts;
    [SerializeField] private TextMeshProUGUI[] _enemyEquationTexts;

    [SerializeField] private LaneManager _laneManager;
    [SerializeField] private EnemyManager _enemyManager;

    void OnEnable()
    {
        EventBus.Subscribe<OnCardPlayed>(HandleCardPlayed);
        EventBus.Subscribe<OnTurnStarted>(HandleTurnStarted);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnCardPlayed>(HandleCardPlayed);
        EventBus.Unsubscribe<OnTurnStarted>(HandleTurnStarted);
    }

    private void HandleTurnStarted(OnTurnStarted evt) => RefreshAll();

    private void HandleCardPlayed(OnCardPlayed evt) => RefreshLane(evt.LaneIndex);

    private void RefreshAll()
    {
        for (int i = 0; i < 3; i++)
            RefreshLane(i);
    }

    private void RefreshLane(int laneIndex)
    {
        Lane lane = _laneManager.GetLane(laneIndex);
        _playerValueTexts[laneIndex].text = lane.PlayerValue.ToString("0.#");

        List<float> equation = _enemyManager.GetLaneEquation(laneIndex);
        _enemyEquationTexts[laneIndex].text = BuildEquationString(equation);
    }

    private string BuildEquationString(List<float> terms)
    {
        return string.Join(" × ", terms);
    }
}