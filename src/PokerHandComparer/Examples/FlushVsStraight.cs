namespace PokerHandComparer.Examples;

public class FlushVsStraight
{
    public void Run()
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

        Console.WriteLine($"Hand 1: {flush}");
        Console.WriteLine($"Hand 2: {straight}");
        Console.WriteLine(HandComparer.GetResult(flush, straight));
    }
}