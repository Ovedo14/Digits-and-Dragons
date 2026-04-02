using UnityEngine;

public class FlowController : MonoBehaviour
{
    [SerializeField] private CombatManager _combatManager;
    [SerializeField] private EnemyManager _enemyManager;

    [Header("Enemy Pool")]
    [SerializeField] private EnemyData[] _enemyPool;

    void Start()
    {
        StartNextCombat();
    }
    void OnEnable()
    {
        EventBus.Subscribe<OnCombatCompleted>(HandleCombatCompleted);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnCombatCompleted>(HandleCombatCompleted);
    }

    //Called by character selection screen
    public void StartRun(CharacterData character)
    {
        RunManager.Instance.InitializeRun(character);
        StartNextCombat();
    }

    private void StartNextCombat()
    {
        EnemyData enemy = PickNextEnemy();
        _enemyManager.SetEnemy(enemy);
        _combatManager.StartCombat();
    }

    private EnemyData PickNextEnemy()
    {
        if (RunManager.Instance.IsFinalBoss)
            return _enemyPool[_enemyPool.Length - 1]; // last entry is always the boss

        return _enemyPool[Random.Range(0, _enemyPool.Length - 1)];
    }

    private void HandleCombatCompleted(OnCombatCompleted evt)
    {
        // Events and run progression go here later
        RunManager.Instance.CombatCount++;
        StartNextCombat();
    }
}