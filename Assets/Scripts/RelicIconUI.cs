using UnityEngine;
using UnityEngine.UI;

public class RelicIconUI : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void Setup(RelicData relic)
    {
        icon.sprite = relic.Icon;
    }
}