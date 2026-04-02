using UnityEngine;

public class LaneSelectionUI : MonoBehaviour
{
    [SerializeField] private CardHandUI _cardHandUI;
    [SerializeField] private int _laneIndex;

    public void OnLaneClicked()
    {
        _cardHandUI.OnLaneSelected(_laneIndex);
    }
}