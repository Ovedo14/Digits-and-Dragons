//Test to verify that cards are being applied correctly to lanes

using NUnit.Framework;
using UnityEngine;

public class CardDataTests
{
    private CardData _addCard;
    private CardData _multiplyCard;

    [SetUp]
    public void Setup()
    {
        _addCard = ScriptableObject.CreateInstance<CardData>();
        _addCard.Operation = CardOperation.Add;
        _addCard.Value = 10;

        _multiplyCard = ScriptableObject.CreateInstance<CardData>();
        _multiplyCard.Operation = CardOperation.Multiply;
        _multiplyCard.Value = 2;
    }

    [Test]
    public void CardData_AddOperation_ReturnsCorrectValue()
    {
        float initialValue = 5;

        float result = _addCard.ApplyTo(initialValue);
        Assert.AreEqual(15, result, "addition didnt add card value to lane value");
    }

    [Test]
    public void CardData_MultiplyOperation_ReturnsCorrectValue()
    {
        float initialValue = 5;

        float result = _multiplyCard.ApplyTo(initialValue);
        Assert.AreEqual(10, result, "multiplication didnt multiply lane value by card value");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_addCard);
        Object.DestroyImmediate(_multiplyCard);
    }
}