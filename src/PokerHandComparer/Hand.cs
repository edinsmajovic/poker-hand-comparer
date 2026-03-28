namespace PokerHandComparer;

public class Hand : IComparable<Hand>
{
    public List<Card> Cards { get; }
    public HandRanking Ranking { get; }

    /// <summary>
    /// Kickers ordered by significance for tie-breaking.
    /// For example, for a Pair of Kings with A-9-5 kickers:
    /// kickers = [King, Ace, Nine, Five]
    /// </summary>
    private readonly List<CardValue> _kickers;

    public Hand(List<Card> cards)
    {
        if (cards.Count != 5)
            throw new ArgumentException("A poker hand must contain exactly 5 cards.");

        Cards = cards.OrderByDescending(c => c.Value).ToList();
        (Ranking, _kickers) = Evaluate();
    }

    private (HandRanking ranking, List<CardValue> kickers) Evaluate()
    {
        var isFlush = Cards.All(c => c.Suit == Cards[0].Suit);
        var isStraight = IsStraight(out var straightHighCard);

        // Royal Flush
        if (isFlush && isStraight && straightHighCard == CardValue.Ace)
            return (HandRanking.RoyalFlush, new List<CardValue> { CardValue.Ace });

        // Straight Flush
        if (isFlush && isStraight)
            return (HandRanking.StraightFlush, new List<CardValue> { straightHighCard });

        var groups = Cards
            .GroupBy(c => c.Value)
            .OrderByDescending(g => g.Count())
            .ThenByDescending(g => g.Key)
            .ToList();

        var groupCounts = groups.Select(g => g.Count()).ToList();

        // Four of a Kind
        if (groupCounts is [4, 1])
        {
            return (HandRanking.FourOfAKind, groups.Select(g => g.Key).ToList());
        }

        // Full House
        if (groupCounts is [3, 2])
        {
            return (HandRanking.FullHouse, groups.Select(g => g.Key).ToList());
        }

        // Flush
        if (isFlush)
        {
            return (HandRanking.Flush, Cards.Select(c => c.Value).ToList());
        }

        // Straight
        if (isStraight)
        {
            return (HandRanking.Straight, new List<CardValue> { straightHighCard });
        }

        // Three of a Kind
        if (groupCounts is [3, 1, 1])
        {
            return (HandRanking.ThreeOfAKind, groups.Select(g => g.Key).ToList());
        }

        // Two Pair
        if (groupCounts is [2, 2, 1])
        {
            return (HandRanking.TwoPair, groups.Select(g => g.Key).ToList());
        }

        // Pair
        if (groupCounts is [2, 1, 1, 1])
        {
            return (HandRanking.Pair, groups.Select(g => g.Key).ToList());
        }

        // High Card
        return (HandRanking.HighCard, Cards.Select(c => c.Value).ToList());
    }

    private bool IsStraight(out CardValue highCard)
    {
        var values = Cards.Select(c => (int)c.Value).Distinct().OrderByDescending(v => v).ToList();

        if (values.Count != 5)
        {
            highCard = Cards[0].Value;
            return false;
        }

        // Normal straight: consecutive values
        if (values[0] - values[4] == 4)
        {
            highCard = (CardValue)values[0];
            return true;
        }

        // Ace-low straight (A-2-3-4-5): Ace=14, Five=5, Four=4, Three=3, Two=2
        if (values is [14, 5, 4, 3, 2])
        {
            highCard = CardValue.Five;
            return true;
        }

        highCard = Cards[0].Value;
        return false;
    }

    public int CompareTo(Hand? other)
    {
        if (other is null)
            return 1;

        var rankComparison = Ranking.CompareTo(other.Ranking);
        if (rankComparison != 0)
            return rankComparison;

        // Same ranking — compare kickers
        for (int i = 0; i < Math.Min(_kickers.Count, other._kickers.Count); i++)
        {
            var kickerComparison = _kickers[i].CompareTo(other._kickers[i]);
            if (kickerComparison != 0)
                return kickerComparison;
        }

        return 0; // Tie
    }

    public override string ToString()
    {
        var cards = string.Join(", ", Cards);
        return $"[{cards}] ({Ranking})";
    }
}
