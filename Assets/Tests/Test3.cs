//Test to verify run manager is working correctly / verify integrity of data contained

using NUnit.Framework;
using UnityEngine;

public class RunManagerTests
{
    private GameObject _runManagerObject;
    private RunManager _runManager;

    [SetUp]
    public void Setup()
    {
        _runManagerObject = new GameObject();
        _runManager = _runManagerObject.AddComponent<RunManager>();
    }

    [Test]
    public void RunManager_ApplyDamage_ReducesPlayerHP()
    {
        _runManager.PlayerHP = 100;
        _runManager.PlayerMaxHP = 100;

        _runManager.ApplyDamage(25);

        Assert.AreEqual(75, _runManager.PlayerHP, "player hp didnt decrease by damage amount");
    }

    [Test]
    public void RunManager_HealPlayer_IncreasesPlayerHP()
    {
        _runManager.PlayerHP = 50;
        _runManager.PlayerMaxHP = 100;

        _runManager.HealPlayer(25);

        Assert.AreEqual(75, _runManager.PlayerHP, "player hp didnt increase by heal amount");
    }

    [Test]
    public void RunManager_HealPlayer_CannotExceedMaxHP()
    {
        _runManager.PlayerHP = 90;
        _runManager.PlayerMaxHP = 100;

        _runManager.HealPlayer(25);

        Assert.AreEqual(100, _runManager.PlayerHP, "player exceeded its max hp");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_runManagerObject);
    }
}