using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardScreenUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _relicText;
    [SerializeField] private Image _relicIcon;
    [SerializeField] private GameObject _relicContainer;

    public void ShowRewards(EnemyData defeatedEnemy)
    {
        RunManager.Instance.Gold += defeatedEnemy.GoldReward;
        _goldText.text = "+" + defeatedEnemy.GoldReward + " Gold";

        RunManager.Instance.HealPlayer(defeatedEnemy.HealthReward);
        _healthText.text = "+" + defeatedEnemy.HealthReward + " HP";

        if (defeatedEnemy.RelicDrop != null)
        {
            RelicManager.Instance.AddRelic(defeatedEnemy.RelicDrop);
            _relicText.text = defeatedEnemy.RelicDrop.RelicName;
            _relicIcon.sprite = defeatedEnemy.RelicDrop.Icon;
            _relicContainer.SetActive(true);
        }
        else
        {
            _relicContainer.SetActive(false);
        }
    }

    public void OnContinuePressed()
    {
        EventBus.Publish(new OnRewardsClaimed());
    }
}