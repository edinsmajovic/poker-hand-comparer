namespace PokerHandComparer.Examples;

public class FullHouseVsFlush
{
    public void Run()
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

        Console.WriteLine($"Hand 1: {fullHouse}");
        Console.WriteLine($"Hand 2: {flush}");
        Console.WriteLine(HandComparer.GetResult(fullHouse, flush));
    }
}