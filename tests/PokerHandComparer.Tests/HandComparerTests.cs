namespace PokerHandComparer.Tests;

public class HandComparerTests
{
    [Fact]
    public void Compare_HigherRankWins()
    {
        var flush = new Hand(new[]
        {
            new Card(CardSuit.H, CardValue.Two),
            new Card(CardSuit.H, CardValue.Five),
            new Card(CardSuit.H, CardValue.Seven),
            new Card(CardSuit.H, CardValue.Nine),
            new Card(CardSuit.H, CardValue.Jack)
        });

        var straight = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Six),
            new Card(CardSuit.D, CardValue.Seven),
            new Card(CardSuit.H, CardValue.Eight),
            new Card(CardSuit.S, CardValue.Nine),
            new Card(CardSuit.C, CardValue.Ten)
        });

        Assert.Equal(1, HandComparer.Compare(flush, straight));
    }

    [Fact]
    public void Compare_FlushVsFlush_HighCardDecides()
    {
        var higherFlush = new Hand(new[]
        {
            new Card(CardSuit.H, CardValue.Three),
            new Card(CardSuit.H, CardValue.Six),
            new Card(CardSuit.H, CardValue.Eight),
            new Card(CardSuit.H, CardValue.Ten),
            new Card(CardSuit.H, CardValue.Ace)
        });

        var lowerFlush = new Hand(new[]
        {
            new Card(CardSuit.D, CardValue.Three),
            new Card(CardSuit.D, CardValue.Six),
            new Card(CardSuit.D, CardValue.Eight),
            new Card(CardSuit.D, CardValue.Ten),
            new Card(CardSuit.D, CardValue.King)
        });

        Assert.Equal(1, HandComparer.Compare(higherFlush, lowerFlush));
    }

    [Fact]
    public void Compare_PairVsPair_HigherPairWins()
    {
        var pairOfAces = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Ace),
            new Card(CardSuit.D, CardValue.Ace),
            new Card(CardSuit.H, CardValue.Three),
            new Card(CardSuit.S, CardValue.Five),
            new Card(CardSuit.C, CardValue.Seven)
        });

        var pairOfKings = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.King),
            new Card(CardSuit.D, CardValue.King),
            new Card(CardSuit.H, CardValue.Three),
            new Card(CardSuit.S, CardValue.Five),
            new Card(CardSuit.C, CardValue.Seven)
        });

        Assert.Equal(1, HandComparer.Compare(pairOfAces, pairOfKings));
    }

    [Fact]
    public void Compare_PairVsPair_SamePair_KickerDecides()
    {
        var pairAcesJackKicker = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Ace),
            new Card(CardSuit.D, CardValue.Ace),
            new Card(CardSuit.H, CardValue.Jack),
            new Card(CardSuit.S, CardValue.Seven),
            new Card(CardSuit.C, CardValue.Three)
        });

        var pairAcesTenKicker = new Hand(new[]
        {
            new Card(CardSuit.S, CardValue.Ace),
            new Card(CardSuit.H, CardValue.Ace),
            new Card(CardSuit.D, CardValue.Ten),
            new Card(CardSuit.C, CardValue.Seven),
            new Card(CardSuit.H, CardValue.Three)
        });

        Assert.Equal(1, HandComparer.Compare(pairAcesJackKicker, pairAcesTenKicker));
    }

    [Fact]
    public void Compare_TwoPairs_HigherTopPairWins()
    {
        var acesAndNines = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Ace),
            new Card(CardSuit.D, CardValue.Ace),
            new Card(CardSuit.H, CardValue.Nine),
            new Card(CardSuit.S, CardValue.Nine),
            new Card(CardSuit.C, CardValue.Two)
        });

        var kingsAndNines = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.King),
            new Card(CardSuit.D, CardValue.King),
            new Card(CardSuit.H, CardValue.Nine),
            new Card(CardSuit.S, CardValue.Nine),
            new Card(CardSuit.C, CardValue.Two)
        });

        Assert.Equal(1, HandComparer.Compare(acesAndNines, kingsAndNines));
    }

    [Fact]
    public void Compare_TwoPairs_SamePairs_KickerDecides()
    {
        var acesNinesKingKicker = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Ace),
            new Card(CardSuit.D, CardValue.Ace),
            new Card(CardSuit.H, CardValue.Nine),
            new Card(CardSuit.S, CardValue.Nine),
            new Card(CardSuit.C, CardValue.King)
        });

        var acesNinesTwoKicker = new Hand(new[]
        {
            new Card(CardSuit.S, CardValue.Ace),
            new Card(CardSuit.H, CardValue.Ace),
            new Card(CardSuit.D, CardValue.Nine),
            new Card(CardSuit.C, CardValue.Nine),
            new Card(CardSuit.H, CardValue.Two)
        });

        Assert.Equal(1, HandComparer.Compare(acesNinesKingKicker, acesNinesTwoKicker));
    }

    [Fact]
    public void Compare_FullHouseVsFlush_FullHouseWins()
    {
        var fullHouse = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.King),
            new Card(CardSuit.D, CardValue.King),
            new Card(CardSuit.H, CardValue.King),
            new Card(CardSuit.S, CardValue.Queen),
            new Card(CardSuit.C, CardValue.Queen)
        });

        var flush = new Hand(new[]
        {
            new Card(CardSuit.D, CardValue.Two),
            new Card(CardSuit.D, CardValue.Five),
            new Card(CardSuit.D, CardValue.Eight),
            new Card(CardSuit.D, CardValue.Ten),
            new Card(CardSuit.D, CardValue.Ace)
        });

        Assert.Equal(1, HandComparer.Compare(fullHouse, flush));
    }

    [Fact]
    public void Compare_IdenticalHands_ReturnsTie()
    {
        var hand1 = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Ace),
            new Card(CardSuit.D, CardValue.King),
            new Card(CardSuit.H, CardValue.Queen),
            new Card(CardSuit.S, CardValue.Jack),
            new Card(CardSuit.C, CardValue.Nine)
        });

        var hand2 = new Hand(new[]
        {
            new Card(CardSuit.S, CardValue.Ace),
            new Card(CardSuit.H, CardValue.King),
            new Card(CardSuit.D, CardValue.Queen),
            new Card(CardSuit.C, CardValue.Jack),
            new Card(CardSuit.H, CardValue.Nine)
        });

        Assert.Equal(0, HandComparer.Compare(hand1, hand2));
    }

    [Fact]
    public void GetResult_ReturnsCorrectString_Hand1Wins()
    {
        var fullHouse = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.King),
            new Card(CardSuit.D, CardValue.King),
            new Card(CardSuit.H, CardValue.King),
            new Card(CardSuit.S, CardValue.Queen),
            new Card(CardSuit.C, CardValue.Queen)
        });

        var pair = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Ace),
            new Card(CardSuit.D, CardValue.Ace),
            new Card(CardSuit.H, CardValue.Three),
            new Card(CardSuit.S, CardValue.Five),
            new Card(CardSuit.C, CardValue.Seven)
        });

        Assert.Equal("Hand 1 has won!", HandComparer.GetResult(fullHouse, pair));
    }

    [Fact]
    public void GetResult_ReturnsCorrectString_Tie()
    {
        var hand1 = new Hand(new[]
        {
            new Card(CardSuit.C, CardValue.Ace),
            new Card(CardSuit.D, CardValue.King),
            new Card(CardSuit.H, CardValue.Queen),
            new Card(CardSuit.S, CardValue.Jack),
            new Card(CardSuit.C, CardValue.Nine)
        });

        var hand2 = new Hand(new[]
        {
            new Card(CardSuit.S, CardValue.Ace),
            new Card(CardSuit.H, CardValue.King),
            new Card(CardSuit.D, CardValue.Queen),
            new Card(CardSuit.C, CardValue.Jack),
            new Card(CardSuit.H, CardValue.Nine)
        });

        Assert.Equal("It's a tie!", HandComparer.GetResult(hand1, hand2));
    }
}