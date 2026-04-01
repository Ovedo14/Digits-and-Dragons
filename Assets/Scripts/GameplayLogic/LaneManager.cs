using UnityEngine;

public class LaneManager : MonoBehaviour
{
    private Lane[] _lanes = new Lane[3];

    void OnEnable()
    {
        EventBus.Subscribe<OnCardPlayed>(HandleCardPlayed);
        EventBus.Subscribe<OnTurnEnded>(HandleTurnEnded);
        EventBus.Subscribe<OnTurnStarted>(HandleTurnStarted);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnCardPlayed>(HandleCardPlayed);
        EventBus.Unsubscribe<OnTurnEnded>(HandleTurnEnded);
        EventBus.Unsubscribe<OnTurnStarted>(HandleTurnStarted);
    }

    private void HandleTurnStarted(OnTurnStarted evt)
    {
        ResetLanes();
    } 

    private void HandleCardPlayed(OnCardPlayed evt)
    {
        _lanes[evt.LaneIndex].PlayerValue = 
            evt.Card.ApplyTo(_lanes[evt.LaneIndex].PlayerValue); //Adds card value to lane
    }

    private void HandleTurnEnded(OnTurnEnded evt) 
    {
        ResolveLanes();
    }

    private void ResolveLanes()
    {
        float[] results = new float[3];
        float playerDamage = 0;
        float enemyDamage = 0;

        for (int i = 0; i < 3; i++)
        {
            results[i] = _lanes[i].PlayerValue - _lanes[i].EnemyValue;

            if (results[i] > 0)
                enemyDamage += results[i];
            else if (results[i] < 0)
                playerDamage += Mathf.Abs(results[i]);
        }

        EventBus.Publish(new OnLanesResolved { LaneResults = results });

        if (enemyDamage > 0)
            EventBus.Publish(new OnDamageDealt { Amount = enemyDamage, ToPlayer = false });

        if (playerDamage > 0)
            EventBus.Publish(new OnDamageDealt { Amount = playerDamage, ToPlayer = true });
    }

    private void ResetLanes()
    {
        for (int i = 0; i < 3; i++)
            _lanes[i] = new Lane();
    }

    //Called by EnemyManager after it decides its values
    public void SetEnemyValue(int laneIndex, float value)
    {
        _lanes[laneIndex].EnemyValue = value;
    }
    public Lane GetLane(int laneIndex) => _lanes[laneIndex]; //used for UI
}