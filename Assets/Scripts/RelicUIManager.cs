using UnityEngine;
using System.Collections.Generic;

public class RelicUIManager : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject relicPrefab;

    private List<GameObject> _spawnedIcons = new List<GameObject>();

    void Start()
    {
        RefreshUI();
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