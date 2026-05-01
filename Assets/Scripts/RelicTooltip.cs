using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class RelicTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _tooltipPanel;
    [SerializeField] private TextMeshProUGUI _tooltipText;
    
    private RelicData _relic;

    public void Setup(RelicData relic)
    {
        _relic = relic;
        _tooltipPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_relic == null) return;
        
        _tooltipText.text = GetRelicDescription();
        _tooltipPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipPanel.SetActive(false);
    }

    private string GetRelicDescription()
    {
        return _relic.Description;
    }
}