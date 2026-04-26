using UnityEngine;

public class FlowController : MonoBehaviour
{
    [SerializeField] private CombatManager _combatManager;
    [SerializeField] private EnemyManager _enemyManager;

    [Header("Screens")]
    [SerializeField] private GameObject _combatScreen;
    [SerializeField] private GameObject _eventScreen;

    [Header("Enemy Pool")]
    [SerializeField] private EnemyStagePool[] _enemyStages;
    [SerializeField] private EnemyData _bossEnemy;

    [Header("Event Pool")]
    [SerializeField] private GameEvent[] _eventPool;
    [SerializeField] private EventUI _eventUI;

    [Header("End Screens")]
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    void OnEnable()
    {
        EventBus.Subscribe<OnCombatCompleted>(HandleCombatCompleted);
        EventBus.Subscribe<OnEventCompleted>(OnEventFinished);
        EventBus.Subscribe<OnPlayerWon>(HandlePlayerWon);
        EventBus.Subscribe<OnPlayerLost>(HandlePlayerLost);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnCombatCompleted>(HandleCombatCompleted);
        EventBus.Unsubscribe<OnEventCompleted>(OnEventFinished);
        EventBus.Unsubscribe<OnPlayerWon>(HandlePlayerWon);
        EventBus.Unsubscribe<OnPlayerLost>(HandlePlayerLost);
    }


    void Start()
    {
        ShowCombat();
        StartNextCombat();
    }

    private void HandlePlayerWon(OnPlayerWon evt)
    {
        _winScreen.SetActive(true);
    }

    private void HandlePlayerLost(OnPlayerLost evt)
    {
        _loseScreen.SetActive(true);
    }

    private void HandleCombatCompleted(OnCombatCompleted evt)
    {
        RunManager.Instance.CombatCount++;
        ShowRandomEvent();
    }

    //Called by Canvas Button on the event screen
    private void OnEventFinished(OnEventCompleted evt)
    {
        StartNextCombat();
        ShowCombat();
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

    private void ShowRandomEvent()
    {
        GameEvent randomEvent = _eventPool[Random.Range(0, _eventPool.Length)];
        _eventUI.ShowEvent(randomEvent);
        ShowEvent();
    }
    private void ShowEvent()
    {
        _eventScreen.SetActive(true);
        _combatScreen.SetActive(false);
    }

    private EnemyData PickNextEnemy()
    {
        if (RunManager.Instance.IsFinalBoss)
            return _bossEnemy;

        int stageIndex = RunManager.Instance.CombatCount;

        stageIndex = Mathf.Clamp(stageIndex, 0, _enemyStages.Length - 1);

        EnemyData[] pool = _enemyStages[stageIndex].enemies;

        return pool[Random.Range(0, pool.Length)];
    }
}