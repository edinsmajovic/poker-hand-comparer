using Xunit;
using PokerHandComparer;

namespace PokerHandComparer.Tests;

public class CardTests
{
    [Fact]
    public void Card_Constructor_SetsProperties()
    {
        var card = new Card(CardSuit.H, CardValue.Ace);

        Assert.Equal(CardSuit.H, card.Suit);
        Assert.Equal(CardValue.Ace, card.Value);
    }

    [Fact]
    public void Card_ToString_ReturnsExpectedFormat()
    {
        var card = new Card(CardSuit.S, CardValue.King);

        Assert.Equal("King of S", card.ToString());
    }
}

public class HandRankingTests
{
    private static Hand MakeHand(params (CardSuit suit, CardValue value)[] cards)
    {
        return new Hand(cards.Select(c => new Card(c.suit, c.value)).ToList());
    }

    [Fact]
    public void Hand_RequiresExactlyFiveCards()
    {
        Assert.Throws<ArgumentException>(() =>
            new Hand(new List<Card> { new(CardSuit.H, CardValue.Ace) }));
    }

    [Fact]
    public void Evaluate_RoyalFlush()
    {
        var hand = MakeHand(
            (CardSuit.H, CardValue.Ten),
            (CardSuit.H, CardValue.Jack),
            (CardSuit.H, CardValue.Queen),
            (CardSuit.H, CardValue.King),
            (CardSuit.H, CardValue.Ace));

        Assert.Equal(HandRanking.RoyalFlush, hand.Ranking);
    }

    [Fact]
    public void Evaluate_StraightFlush()
    {
        var hand = MakeHand(
            (CardSuit.D, CardValue.Five),
            (CardSuit.D, CardValue.Six),
            (CardSuit.D, CardValue.Seven),
            (CardSuit.D, CardValue.Eight),
            (CardSuit.D, CardValue.Nine));

        Assert.Equal(HandRanking.StraightFlush, hand.Ranking);
    }

    [Fact]
    public void Evaluate_FourOfAKind()
    {
        var hand = MakeHand(
            (CardSuit.C, CardValue.Jack),
            (CardSuit.D, CardValue.Jack),
            (CardSuit.H, CardValue.Jack),
            (CardSuit.S, CardValue.Jack),
            (CardSuit.C, CardValue.Two));

        Assert.Equal(HandRanking.FourOfAKind, hand.Ranking);
    }

    [Fact]
    public void Evaluate_FullHouse()
    {
        var hand = MakeHand(
            (CardSuit.C, CardValue.King),
            (CardSuit.D, CardValue.King),
            (CardSuit.H, CardValue.King),
            (CardSuit.S, CardValue.Four),
            (CardSuit.C, CardValue.Four));

        Assert.Equal(HandRanking.FullHouse, hand.Ranking);
    }

    [Fact]
    public void Evaluate_Flush()
    {
        var hand = MakeHand(
            (CardSuit.S, CardValue.Two),
            (CardSuit.S, CardValue.Five),
            (CardSuit.S, CardValue.Eight),
            (CardSuit.S, CardValue.Jack),
            (CardSuit.S, CardValue.Ace));

        Assert.Equal(HandRanking.Flush, hand.Ranking);
    }

    [Fact]
    public void Evaluate_Straight()
    {
        var hand = MakeHand(
            (CardSuit.C, CardValue.Three),
            (CardSuit.D, CardValue.Four),
            (CardSuit.H, CardValue.Five),
            (CardSuit.S, CardValue.Six),
            (CardSuit.C, CardValue.Seven));

        Assert.Equal(HandRanking.Straight, hand.Ranking);
    }

    [Fact]
    public void Evaluate_AceLowStraight()
    {
        var hand = MakeHand(
            (CardSuit.C, CardValue.Ace),
            (CardSuit.D, CardValue.Two),
            (CardSuit.H, CardValue.Three),
            (CardSuit.S, CardValue.Four),
            (CardSuit.C, CardValue.Five));

        Assert.Equal(HandRanking.Straight, hand.Ranking);
    }

    [Fact]
    public void Evaluate_ThreeOfAKind()
    {
        var hand = MakeHand(
            (CardSuit.C, CardValue.Nine),
            (CardSuit.D, CardValue.Nine),
            (CardSuit.H, CardValue.Nine),
            (CardSuit.S, CardValue.Three),
            (CardSuit.C, CardValue.Seven));

        Assert.Equal(HandRanking.ThreeOfAKind, hand.Ranking);
    }

    [Fact]
    public void Evaluate_TwoPair()
    {
        var hand = MakeHand(
            (CardSuit.C, CardValue.Ten),
            (CardSuit.D, CardValue.Ten),
            (CardSuit.H, CardValue.Six),
            (CardSuit.S, CardValue.Six),
            (CardSuit.C, CardValue.Ace));

        Assert.Equal(HandRanking.TwoPair, hand.Ranking);
    }

    [Fact]
    public void Evaluate_Pair()
    {
        var hand = MakeHand(
            (CardSuit.C, CardValue.Queen),
            (CardSuit.D, CardValue.Queen),
            (CardSuit.H, CardValue.Three),
            (CardSuit.S, CardValue.Seven),
            (CardSuit.C, CardValue.King));

        Assert.Equal(HandRanking.Pair, hand.Ranking);
    }

    [Fact]
    public void Evaluate_HighCard()
    {
        var hand = MakeHand(
            (CardSuit.C, CardValue.Two),
            (CardSuit.D, CardValue.Five),
            (CardSuit.H, CardValue.Eight),
            (CardSuit.S, CardValue.Jack),
            (CardSuit.C, CardValue.King));

        Assert.Equal(HandRanking.HighCard, hand.Ranking);
    }
}

public class HandComparisonTests
{
    private static Hand MakeHand(params (CardSuit suit, CardValue value)[] cards)
    {
        return new Hand(cards.Select(c => new Card(c.suit, c.value)).ToList());
    }

    [Fact]
    public void HigherRanking_Wins()
    {
        var flush = MakeHand(
            (CardSuit.S, CardValue.Two),
            (CardSuit.S, CardValue.Five),
            (CardSuit.S, CardValue.Eight),
            (CardSuit.S, CardValue.Jack),
            (CardSuit.S, CardValue.Ace));

        var pair = MakeHand(
            (CardSuit.C, CardValue.Ace),
            (CardSuit.D, CardValue.Ace),
            (CardSuit.H, CardValue.Three),
            (CardSuit.S, CardValue.Seven),
            (CardSuit.C, CardValue.King));

        Assert.True(flush.CompareTo(pair) > 0);
        Assert.True(pair.CompareTo(flush) < 0);
    }

    [Fact]
    public void SameRanking_HigherKicker_Wins()
    {
        var pairOfAces = MakeHand(
            (CardSuit.C, CardValue.Ace),
            (CardSuit.D, CardValue.Ace),
            (CardSuit.H, CardValue.Three),
            (CardSuit.S, CardValue.Seven),
            (CardSuit.C, CardValue.King));

        var pairOfKings = MakeHand(
            (CardSuit.C, CardValue.King),
            (CardSuit.D, CardValue.King),
            (CardSuit.H, CardValue.Three),
            (CardSuit.S, CardValue.Seven),
            (CardSuit.C, CardValue.Ace));

        Assert.True(pairOfAces.CompareTo(pairOfKings) > 0);
    }

    [Fact]
    public void SameRanking_SameKickers_IsTie()
    {
        var hand1 = MakeHand(
            (CardSuit.C, CardValue.Ace),
            (CardSuit.D, CardValue.King),
            (CardSuit.H, CardValue.Queen),
            (CardSuit.S, CardValue.Jack),
            (CardSuit.C, CardValue.Nine));

        var hand2 = MakeHand(
            (CardSuit.H, CardValue.Ace),
            (CardSuit.S, CardValue.King),
            (CardSuit.D, CardValue.Queen),
            (CardSuit.C, CardValue.Jack),
            (CardSuit.H, CardValue.Nine));

        Assert.Equal(0, hand1.CompareTo(hand2));
    }

    [Fact]
    public void AceLowStraight_LosesTo_HigherStraight()
    {
        var aceLow = MakeHand(
            (CardSuit.C, CardValue.Ace),
            (CardSuit.D, CardValue.Two),
            (CardSuit.H, CardValue.Three),
            (CardSuit.S, CardValue.Four),
            (CardSuit.C, CardValue.Five));

        var sixHigh = MakeHand(
            (CardSuit.C, CardValue.Two),
            (CardSuit.D, CardValue.Three),
            (CardSuit.H, CardValue.Four),
            (CardSuit.S, CardValue.Five),
            (CardSuit.C, CardValue.Six));

        Assert.True(aceLow.CompareTo(sixHigh) < 0);
    }

    [Fact]
    public void FourOfAKind_BeatsFullHouse()
    {
        var fourOfAKind = MakeHand(
            (CardSuit.C, CardValue.Two),
            (CardSuit.D, CardValue.Two),
            (CardSuit.H, CardValue.Two),
            (CardSuit.S, CardValue.Two),
            (CardSuit.C, CardValue.Three));

        var fullHouse = MakeHand(
            (CardSuit.C, CardValue.Ace),
            (CardSuit.D, CardValue.Ace),
            (CardSuit.H, CardValue.Ace),
            (CardSuit.S, CardValue.King),
            (CardSuit.C, CardValue.King));

        Assert.True(fourOfAKind.CompareTo(fullHouse) > 0);
    }

    [Fact]
    public void RoyalFlush_BeatsStraightFlush()
    {
        var royal = MakeHand(
            (CardSuit.H, CardValue.Ten),
            (CardSuit.H, CardValue.Jack),
            (CardSuit.H, CardValue.Queen),
            (CardSuit.H, CardValue.King),
            (CardSuit.H, CardValue.Ace));

        var straightFlush = MakeHand(
            (CardSuit.D, CardValue.Nine),
            (CardSuit.D, CardValue.Ten),
            (CardSuit.D, CardValue.Jack),
            (CardSuit.D, CardValue.Queen),
            (CardSuit.D, CardValue.King));

        Assert.True(royal.CompareTo(straightFlush) > 0);
    }

    [Fact]
    public void FullHouse_HigherTrips_Wins()
    {
        var acesFullOfKings = MakeHand(
            (CardSuit.C, CardValue.Ace),
            (CardSuit.D, CardValue.Ace),
            (CardSuit.H, CardValue.Ace),
            (CardSuit.S, CardValue.King),
            (CardSuit.C, CardValue.King));

        var kingsFullOfAces = MakeHand(
            (CardSuit.C, CardValue.King),
            (CardSuit.D, CardValue.King),
            (CardSuit.H, CardValue.King),
            (CardSuit.S, CardValue.Ace),
            (CardSuit.C, CardValue.Ace));

        Assert.True(acesFullOfKings.CompareTo(kingsFullOfAces) > 0);
    }
}
