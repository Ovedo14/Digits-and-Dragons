using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private LaneManager _laneManager;
    [SerializeField] private EnemyAnimatorUI _enemyAnimatorUI;

    private EnemyData _currentEnemy;
    public int _currentHP;
    private LaneEquation[] _laneEquations = new LaneEquation[3];

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        for (int i = 0; i < 3; i++)
            _laneEquations[i] = new LaneEquation(OperationType.Add, new List<float>());
    }

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
        _enemyAnimatorUI.SetupEnemy(enemy);
    }

    private void HandleTurnStarted(OnTurnStarted evt)
    {
        Debug.Log("EnemyManager received OnTurnStarted");
        GenerateLaneValues();
    }

    private void GenerateLaneValues()
    {
        for (int i = 0; i < 3; i++)
        {
            OperationType opType = PickOperationType(_currentEnemy.Operation);
            _laneEquations[i] = GenerateEquation(_currentEnemy.Bias, opType, i);
            
            float laneTotal = CalculateTotal(_laneEquations[i]);
            _laneManager.SetEnemyValue(i, laneTotal);

            Debug.Log("Publishing OnEnemyEquationsGenerated");
            EventBus.Publish(new OnEnemyEquationsGenerated());
        }
    }

    private OperationType PickOperationType(OperationType enemyOp)
    {
        if (enemyOp == OperationType.All)
        {
            OperationType[] allTypes = { OperationType.Add, OperationType.Mult, OperationType.Div };
            return allTypes[Random.Range(0, allTypes.Length)];
        }
        return enemyOp;
    }

    private LaneEquation GenerateEquation(LaneBias bias, OperationType opType, int laneIndex)
    {
        List<float> terms = new List<float>();

        switch (opType)
        {
            case OperationType.Add:
                AdditionSettings addSettings = _currentEnemy.AddSettings;
                int addTerms = Random.Range(addSettings.MinTerms, addSettings.MaxTerms + 1);
                for (int i = 0; i < addTerms; i++)
                {
                    float value = Random.Range(addSettings.MinValue, addSettings.MaxValue);
                    value = Mathf.Floor(ApplyBias(value, bias, laneIndex));
                    terms.Add(value);
                }
                break;

            case OperationType.Mult:
                MultiplicationSettings multSettings = _currentEnemy.MultSettings;
                int multTerms = Random.Range(multSettings.MinTerms, multSettings.MaxTerms + 1);
                for (int i = 0; i < multTerms; i++)
                {
                    float value = Random.Range(multSettings.MinValue, multSettings.MaxValue);
                    value = Mathf.Floor(ApplyBias(value, bias, laneIndex));
                    terms.Add(value);
                }
                break;

            case OperationType.Div:
                DivisionSettings divSettings = _currentEnemy.DivSettings;
                float dividend = Random.Range(divSettings.MinDividend, divSettings.MaxDividend);
                float divisor = Random.Range(divSettings.MinDivisor, divSettings.MaxDivisor);
                
                dividend = Mathf.Floor(ApplyBias(dividend, bias, laneIndex));
                divisor = Mathf.Max(1, Mathf.Floor(divisor));
                
                terms.Add(dividend);
                terms.Add(divisor);
                break;
        }

        return new LaneEquation(opType, terms);
    }

    private float ApplyBias(float factor, LaneBias bias, int laneIndex)
    {
        switch (bias)
        {
            case LaneBias.Aggressive:
                return laneIndex == 0 ? factor * 1.5f : factor * 0.75f;
            case LaneBias.Defensive:
                return factor * 0.9f;
            case LaneBias.Balanced:
            default:
                return factor;
        }
    }

    private float CalculateTotal(LaneEquation equation)
    {
        switch (equation.OpType)
        {
            case OperationType.Add:
                float sum = 0;
                foreach (float term in equation.Terms)
                    sum += term;
                return sum;

            case OperationType.Mult:
                float product = 1;
                foreach (float term in equation.Terms)
                    product *= term;
                return product;

            case OperationType.Div:
                return equation.Terms[0] / equation.Terms[1];

            default:
                return 0;
        }
    }

    private void HandleDamageDealt(OnDamageDealt evt)
    {
        if (evt.ToPlayer) return;
        _currentHP -= (int)evt.Amount;
        if (_currentHP <= 0)
            EventBus.Publish(new OnCombatEnded { PlayerWon = true });
    }

    public void HandleExtraDmg()
    {
        if (_currentHP <= 0)
            EventBus.Publish(new OnCombatEnded { PlayerWon = true });
    }

    public LaneEquation GetLaneEquation(int laneIndex) => _laneEquations[laneIndex];
    public float GetCurrentHP() => _currentHP;
    public EnemyData GetCurrentEnemy() => _currentEnemy;
}