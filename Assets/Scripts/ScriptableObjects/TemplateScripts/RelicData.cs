using UnityEngine;

public abstract class RelicData : ScriptableObject
{
    public int RelicId;
    public string RelicName;
    [TextArea(2, 4)]
    public string Description;
    public Sprite Icon;
    public RelicTrigger Trigger;
    
    public virtual void OnAcquire() { }
    public abstract void ApplyEffect(RelicContext context);
}