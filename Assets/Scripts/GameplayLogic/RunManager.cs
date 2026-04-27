using UnityEngine;
using System.Collections.Generic;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance { get; private set; }

    [Header("Run State")]
    public float PlayerHP;
    public float PlayerMaxHP;
    public List<RelicData> Relics = new List<RelicData>();
    public int Gold = 0;
    public int CombatCount = 0;

    [Header("Boss Settings")]
    [SerializeField] private int _combatsBeforeBoss = 5;
    public bool IsFinalBoss => CombatCount >= _combatsBeforeBoss;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        EventBus.Subscribe<OnDamageDealt>(HandleDamageDealt);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnDamageDealt>(HandleDamageDealt);
    }

    private void HandleDamageDealt(OnDamageDealt evt)
    {
        if (!evt.ToPlayer) return;
        ApplyDamage(evt.Amount);
    }

    public void ApplyDamage(float amount)
    {
        PlayerHP = Mathf.Max(0, PlayerHP - (int)amount);

        if (PlayerHP <= 0)
            EventBus.Publish(new OnCombatEnded { PlayerWon = false });
    }

    public CharacterData CurrentCharacter { get; private set; }

    public void InitializeRun(CharacterData character)
    {
        CurrentCharacter = character;
        PlayerMaxHP = character.StartingHP;
        PlayerHP = character.StartingHP;
        Relics = new List<RelicData>(character.StartingRelics);
        CombatCount = 0;
    }

    public void HealPlayer(float amount)
    {
        PlayerHP = Mathf.Min(PlayerMaxHP, PlayerHP + amount);
    }
}