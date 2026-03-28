namespace PokerHandComparer;

public class Hand
{
    public IReadOnlyList<Card> Cards { get; }

    public Hand(IEnumerable<Card> cards)
    {
        var list = cards.ToList();
        if (list.Count != 5)
            throw new ArgumentException("A hand must contain exactly 5 cards.");

        Cards = list;
    }

    public override string ToString() => string.Join(", ", Cards);
}