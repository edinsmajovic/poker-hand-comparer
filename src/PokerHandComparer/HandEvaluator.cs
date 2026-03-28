namespace PokerHandComparer;

public static class HandEvaluator
{
    public static HandRank Evaluate(Hand hand)
    {
        var isFlush = IsFlush(hand);
        var isStraight = IsStraight(hand);

        if (isFlush && isStraight) return HandRank.StraightFlush;
        if (HasGroupOfSize(hand, 4)) return HandRank.FourOfAKind;
        if (IsFullHouse(hand)) return HandRank.FullHouse;
        if (isFlush) return HandRank.Flush;
        if (isStraight) return HandRank.Straight;
        if (HasGroupOfSize(hand, 3)) return HandRank.ThreeOfAKind;
        if (CountPairs(hand) == 2) return HandRank.TwoPairs;
        if (CountPairs(hand) == 1) return HandRank.Pair;

        return HandRank.HighCard;
    }

    private static bool IsFlush(Hand hand)
        => hand.Cards.Select(c => c.Suit).Distinct().Count() == 1;

    private static bool IsStraight(Hand hand)
    {
        var values = hand.Cards.Select(c => (int)c.Value).OrderBy(v => v).ToList();

        // ace-low straight: A-2-3-4-5
        if (values.SequenceEqual(new[] { 2, 3, 4, 5, 14 }))
            return true;

        return values.Last() - values.First() == 4 && values.Distinct().Count() == 5;
    }

    private static bool HasGroupOfSize(Hand hand, int size)
        => GroupByValue(hand).Any(g => g.Count() == size);

    private static bool IsFullHouse(Hand hand)
    {
        var groups = GroupByValue(hand).Select(g => g.Count()).OrderBy(c => c).ToList();
        return groups.SequenceEqual(new[] { 2, 3 });
    }

    private static int CountPairs(Hand hand)
        => GroupByValue(hand).Count(g => g.Count() == 2);

    private static IEnumerable<IGrouping<CardValue, Card>> GroupByValue(Hand hand)
        => hand.Cards.GroupBy(c => c.Value);
}