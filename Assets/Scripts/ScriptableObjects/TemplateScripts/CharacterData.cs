using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Character")]
public class CharacterData : ScriptableObject
{
    public int characterId;
    
    [Header("Visuals")]
    public Sprite Portrait;
    public Sprite CharacterSprite;

    [Header("Animations")]
    public AnimationClip IdleAnimation;
    public AnimationClip AttackAnimation;

    [Header("Stats")]
    public int StartingHP;

    [Header("Starting Relics")]
    public List<RelicData> StartingRelics;
}