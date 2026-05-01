using UnityEngine;
using System.Collections.Generic;

public class RelicUIManager : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject relicPrefab;

    private List<GameObject> _spawnedIcons = new List<GameObject>();

    void OnEnable()
    {
        EventBus.Subscribe<OnRelicAdded>(HandleRelicAdded);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnRelicAdded>(HandleRelicAdded);
    }

    private void HandleRelicAdded(OnRelicAdded evt)
    {
        AddSingleRelic(evt.Relic);
    }
    void Start()
    {
        RefreshUI();
    }

    private void AddSingleRelic(RelicData relic)
    {
        GameObject obj = Instantiate(relicPrefab, container);

        RelicIconUI iconUI = obj.GetComponent<RelicIconUI>();
        iconUI.Setup(relic);

        _spawnedIcons.Add(obj);
    }
    
    public void RefreshUI()
    {
        ClearUI();

        foreach (RelicData relic in RunManager.Instance.Relics)
        {
            GameObject obj = Instantiate(relicPrefab, container);

            RelicIconUI iconUI = obj.GetComponent<RelicIconUI>();
            iconUI.Setup(relic);

            _spawnedIcons.Add(obj);
        }
    }

    private void ClearUI()
    {
        foreach (GameObject obj in _spawnedIcons)
        {
            Destroy(obj);
        }

        _spawnedIcons.Clear();
    }
}