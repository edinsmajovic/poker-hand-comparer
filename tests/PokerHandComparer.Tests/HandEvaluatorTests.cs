namespace PokerHandComparer.Tests;

public class HandEvaluatorTests
{
    [Fact]
    public void Evaluate_ReturnsHighCard()
    {
        var hand = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Two),
            new Card(CardSuit.D, CardValue.Five),
            new Card(CardSuit.H, CardValue.Seven),
            new Card(CardSuit.S, CardValue.Nine),
            new Card(CardSuit.C, CardValue.Jack)
        });

        Assert.Equal(HandRank.HighCard, HandEvaluator.Evaluate(hand));
    }

    [Fact]
    public void Evaluate_ReturnsPair()
    {
        var hand = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.King),
            new Card(CardSuit.D, CardValue.King),
            new Card(CardSuit.H, CardValue.Three),
            new Card(CardSuit.S, CardValue.Seven),
            new Card(CardSuit.C, CardValue.Nine)
        });

        Assert.Equal(HandRank.Pair, HandEvaluator.Evaluate(hand));
    }

    [Fact]
    public void Evaluate_ReturnsTwoPairs()
    {
        var hand = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.King),
            new Card(CardSuit.D, CardValue.King),
            new Card(CardSuit.H, CardValue.Nine),
            new Card(CardSuit.S, CardValue.Nine),
            new Card(CardSuit.C, CardValue.Three)
        });

        Assert.Equal(HandRank.TwoPairs, HandEvaluator.Evaluate(hand));
    }

    [Fact]
    public void Evaluate_ReturnsThreeOfAKind()
    {
        var hand = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Queen),
            new Card(CardSuit.D, CardValue.Queen),
            new Card(CardSuit.H, CardValue.Queen),
            new Card(CardSuit.S, CardValue.Four),
            new Card(CardSuit.C, CardValue.Seven)
        });

        Assert.Equal(HandRank.ThreeOfAKind, HandEvaluator.Evaluate(hand));
    }

    [Fact]
    public void Evaluate_ReturnsStraight()
    {
        var hand = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Six),
            new Card(CardSuit.D, CardValue.Seven),
            new Card(CardSuit.H, CardValue.Eight),
            new Card(CardSuit.S, CardValue.Nine),
            new Card(CardSuit.C, CardValue.Ten)
        });

        Assert.Equal(HandRank.Straight, HandEvaluator.Evaluate(hand));
    }

    [Fact]
    public void Evaluate_ReturnsAceLowStraight()
    {
        var hand = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Ace),
            new Card(CardSuit.D, CardValue.Two),
            new Card(CardSuit.H, CardValue.Three),
            new Card(CardSuit.S, CardValue.Four),
            new Card(CardSuit.C, CardValue.Five)
        });

        Assert.Equal(HandRank.Straight, HandEvaluator.Evaluate(hand));
    }

    [Fact]
    public void Evaluate_ReturnsFlush()
    {
        var hand = new Hand(new[]
        {
            new Card(CardSuit.H, CardValue.Two),
            new Card(CardSuit.H, CardValue.Five),
            new Card(CardSuit.H, CardValue.Seven),
            new Card(CardSuit.H, CardValue.Nine),
            new Card(CardSuit.H, CardValue.Jack)
        });

        Assert.Equal(HandRank.Flush, HandEvaluator.Evaluate(hand));
    }

    [Fact]
    public void Evaluate_ReturnsFullHouse()
    {
        var hand = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.King),
            new Card(CardSuit.D, CardValue.King),
            new Card(CardSuit.H, CardValue.King),
            new Card(CardSuit.S, CardValue.Queen),
            new Card(CardSuit.C, CardValue.Queen)
        });

        Assert.Equal(HandRank.FullHouse, HandEvaluator.Evaluate(hand));
    }

    [Fact]
    public void Evaluate_ReturnsFourOfAKind()
    {
        var hand = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Jack),
            new Card(CardSuit.D, CardValue.Jack),
            new Card(CardSuit.H, CardValue.Jack),
            new Card(CardSuit.S, CardValue.Jack),
            new Card(CardSuit.C, CardValue.Three)
        });

        Assert.Equal(HandRank.FourOfAKind, HandEvaluator.Evaluate(hand));
    }

    [Fact]
    public void Evaluate_ReturnsStraightFlush()
    {
        var hand = new Hand(new[]
        {
            new Card(CardSuit.S, CardValue.Six),
            new Card(CardSuit.S, CardValue.Seven),
            new Card(CardSuit.S, CardValue.Eight),
            new Card(CardSuit.S, CardValue.Nine),
            new Card(CardSuit.S, CardValue.Ten)
        });

        Assert.Equal(HandRank.StraightFlush, HandEvaluator.Evaluate(hand));
    }
}