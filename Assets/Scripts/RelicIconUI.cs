using UnityEngine;
using UnityEngine.UI;

public class RelicIconUI : MonoBehaviour
{
    [SerializeField] private Image _icon;

    public void Setup(RelicData relic)
    {
        _icon.sprite = relic.Icon;
        GetComponent<RelicTooltip>()?.Setup(relic);
    }
}