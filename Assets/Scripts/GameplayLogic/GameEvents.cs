// Combat flow
public struct OnTurnStarted { }
public struct OnTurnEnded { }

public struct OnCombatEnded
{
    public bool PlayerWon;
}

// Cards
public struct OnCardPlayed
{
    public CardData Card;
    public int LaneIndex;
}

// Lanes
public struct OnLanesResolved
{
    public int[] LaneResults;
}

// Damage
public struct OnDamageDealt
{
    public int Amount;
    public bool ToPlayer;
}

// Relics
public struct OnRelicTriggered
{
    public RelicData Relic;
}

// Run flow
public struct OnCombatCompleted { }
public struct OnEventCompleted { }