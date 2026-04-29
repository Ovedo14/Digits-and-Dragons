using UnityEngine;

public abstract class RelicData : ScriptableObject
{
    public int RelicId;
    public string RelicName;
    public Sprite Icon;
    public RelicTrigger Trigger;

    public abstract void ApplyEffect( //RelicContext context
    );

    //public class RelicContext //Whatever the relic's logic needs from managers goes here
    //{
    //    public CardData LastCardPlayed;
    //    public int LastLaneIndex;
    //    public int LastDamageAmount;
        
        //faltan cosas probablemente 
    //}
}