using UnityEngine;

public class FlowController : MonoBehaviour
{
    [SerializeField] private CombatManager _combatManager;
    [SerializeField] private EnemyManager _enemyManager;

    [Header("Screens")]
    [SerializeField] private GameObject _combatScreen;
    [SerializeField] private GameObject _eventScreen;

    [Header("Enemy Pool")]
    [SerializeField] private EnemyData[] _enemyPool;

    void OnEnable()
    {
        EventBus.Subscribe<OnCombatCompleted>(HandleCombatCompleted);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnCombatCompleted>(HandleCombatCompleted);
    }

    void Start()
    {
        ShowCombat();
        StartNextCombat();
    }

    private void HandleCombatCompleted(OnCombatCompleted evt)
    {
        RunManager.Instance.CombatCount++;
        ShowEvent();
    }

    // Called by your Canvas Button on the event screen
    public void OnEventFinished()
    {
        ShowCombat();
        StartNextCombat();
    }

    private void StartNextCombat()
    {
        EnemyData enemy = PickNextEnemy();
        _enemyManager.SetEnemy(enemy);
        _combatManager.StartCombat();
    }

    private void ShowCombat()
    {
        _combatScreen.SetActive(true);
        _eventScreen.SetActive(false);
    }

    private void ShowEvent()
    {
        _eventScreen.SetActive(true);
        _combatScreen.SetActive(false);
    }

    private EnemyData PickNextEnemy()
    {
        if (RunManager.Instance.IsFinalBoss)
            return _enemyPool[_enemyPool.Length - 1];

        return _enemyPool[Random.Range(0, _enemyPool.Length - 1)];
    }
}