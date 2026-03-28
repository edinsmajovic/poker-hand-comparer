namespace PokerHandComparer;

public static class HandComparer
{
    // returns 1 if hand1 wins, -1 if hand2 wins, 0 for a tie
    public static int Compare(Hand hand1, Hand hand2)
    {
        var rank1 = HandEvaluator.Evaluate(hand1);
        var rank2 = HandEvaluator.Evaluate(hand2);

        if (rank1 != rank2)
            return rank1.CompareTo(rank2);

        return BreakTie(hand1, hand2, rank1);
    }

    public static string GetResult(Hand hand1, Hand hand2)
    {
        var result = Compare(hand1, hand2);
        return result switch
        {
            1  => "Hand 1 has won!",
            -1 => "Hand 2 has won!",
            _  => "It's a tie!"
        };
    }

    private static int BreakTie(Hand hand1, Hand hand2, HandRank rank)
    {
        return rank switch
        {
            HandRank.StraightFlush  => CompareHighCard(hand1, hand2),
            HandRank.FourOfAKind    => CompareGroupThenKickers(hand1, hand2, 4),
            HandRank.FullHouse      => CompareGroupThenKickers(hand1, hand2, 3),
            HandRank.Flush          => CompareHighCard(hand1, hand2),
            HandRank.Straight       => CompareHighCard(hand1, hand2),
            HandRank.ThreeOfAKind   => CompareGroupThenKickers(hand1, hand2, 3),
            HandRank.TwoPairs       => CompareTwoPairs(hand1, hand2),
            HandRank.Pair           => CompareGroupThenKickers(hand1, hand2, 2),
            _                       => CompareHighCard(hand1, hand2)
        };
    }

    private static int CompareHighCard(Hand hand1, Hand hand2)
    {
        var values1 = hand1.Cards.Select(c => (int)c.Value).OrderByDescending(v => v).ToList();
        var values2 = hand2.Cards.Select(c => (int)c.Value).OrderByDescending(v => v).ToList();

        for (int i = 0; i < values1.Count; i++)
        {
            var cmp = values1[i].CompareTo(values2[i]);
            if (cmp != 0) return cmp;
        }

        return 0;
    }

    private static int CompareGroupThenKickers(Hand hand1, Hand hand2, int groupSize)
    {
        var group1 = GetGroupValue(hand1, groupSize);
        var group2 = GetGroupValue(hand2, groupSize);

        if (group1 != group2) return group1.CompareTo(group2);

        var kickers1 = GetKickers(hand1, groupSize);
        var kickers2 = GetKickers(hand2, groupSize);

        for (int i = 0; i < kickers1.Count; i++)
        {
            var cmp = kickers1[i].CompareTo(kickers2[i]);
            if (cmp != 0) return cmp;
        }

        return 0;
    }

    private static int CompareTwoPairs(Hand hand1, Hand hand2)
    {
        var pairs1 = GetPairValues(hand1);
        var pairs2 = GetPairValues(hand2);

        for (int i = 0; i < pairs1.Count; i++)
        {
            var cmp = pairs1[i].CompareTo(pairs2[i]);
            if (cmp != 0) return cmp;
        }

        var kicker1 = hand1.Cards.Select(c => (int)c.Value)
            .Where(v => !pairs1.Contains(v)).First();
        var kicker2 = hand2.Cards.Select(c => (int)c.Value)
            .Where(v => !pairs2.Contains(v)).First();

        return kicker1.CompareTo(kicker2);
    }

    private static int GetGroupValue(Hand hand, int groupSize)
        => hand.Cards.GroupBy(c => c.Value)
            .Where(g => g.Count() == groupSize)
            .Select(g => (int)g.Key)
            .First();

    private static List<int> GetKickers(Hand hand, int groupSize)
        => hand.Cards.GroupBy(c => c.Value)
            .Where(g => g.Count() != groupSize)
            .Select(g => (int)g.Key)
            .OrderByDescending(v => v)
            .ToList();

    private static List<int> GetPairValues(Hand hand)
        => hand.Cards.GroupBy(c => c.Value)
            .Where(g => g.Count() == 2)
            .Select(g => (int)g.Key)
            .OrderByDescending(v => v)
            .ToList();
}