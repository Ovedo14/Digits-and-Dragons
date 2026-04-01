using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LaneManager _laneManager;

    [Header("Equation Settings")]
    [SerializeField] private int _minTerms = 2;
    [SerializeField] private int _maxTerms = 3;
    [SerializeField] private float _minFactor = 1;
    [SerializeField] private float _maxFactor = 5;

    private EnemyData _currentEnemy;
    private int _currentHP;

    //Stores the equation components per lane for the UI
    private List<float>[] _laneEquations = new List<float>[3];

    void OnEnable()
    {
        EventBus.Subscribe<OnTurnStarted>(HandleTurnStarted);
        EventBus.Subscribe<OnDamageDealt>(HandleDamageDealt);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnTurnStarted>(HandleTurnStarted);
        EventBus.Unsubscribe<OnDamageDealt>(HandleDamageDealt);
    }

    public void SetEnemy(EnemyData enemy)
    {
        _currentEnemy = enemy;
        _currentHP = enemy.MaxHP;
    }
 
    private void HandleTurnStarted(OnTurnStarted evt) 
    { 
        GenerateLaneValues();
    }

    private void GenerateLaneValues()
    {
        for (int i = 0; i < 3; i++)
        {
            _laneEquations[i] = GenerateEquation(_currentEnemy.Bias, i);

            float laneTotal = CalculateTotal(_laneEquations[i]);
            _laneManager.SetEnemyValue(i, laneTotal);
        }
    }

    private List<float> GenerateEquation(LaneBias bias, int laneIndex)
    {
        List<float> terms = new List<float>();
        int termCount = Random.Range(_minTerms, _maxTerms + 1);

        for (int i = 0; i < termCount; i++)
        {
            float factor = Mathf.Round(Random.Range(_minFactor, _maxFactor) * 10f) / 10f;

            factor = ApplyBias(factor, bias, laneIndex);
            terms.Add(factor);
        }

        return terms;
    }

    private float ApplyBias(float factor, LaneBias bias, int laneIndex)
    {
        switch (bias)
        {
            //Aggressive - attacks heavily in one lane
            case LaneBias.Aggressive:
                return laneIndex == 0 ? factor * 1.5f : factor * 0.75f;

            //Defensive - spreads evenly with nerfed damage
            case LaneBias.Defensive:
                return factor * 0.9f;

            //Balanced - no modification to generated equation
            case LaneBias.Balanced:
            default:
                return factor;
        }
    }

    private float CalculateTotal(List<float> terms)
    {
        float total = 1;
        foreach (float term in terms)
            total *= term;
        return total;
    }

    private void HandleDamageDealt(OnDamageDealt evt)
    {
        if (evt.ToPlayer) return;

        _currentHP -= (int)evt.Amount;

        if (_currentHP <= 0)
            EventBus.Publish(new OnCombatEnded { PlayerWon = true });
    }

    // UI reads this to render the equation for a given lane
    public List<float> GetLaneEquation(int laneIndex) => _laneEquations[laneIndex];
    public float GetCurrentHP() => _currentHP;
}