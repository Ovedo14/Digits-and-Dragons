using UnityEngine;

public abstract class RelicData : ScriptableObject
{
    public string RelicName;
    public Sprite Icon;
    public RelicTrigger Trigger;

    public abstract void ApplyEffect();
}