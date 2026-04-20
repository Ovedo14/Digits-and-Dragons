using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/GameEvent")]
public class GameEvent : ScriptableObject
{
    public string Title;
    [TextArea(3, 6)]
    public string Description;
    public Sprite EventImage;
    public List<EventOption> Options;
}