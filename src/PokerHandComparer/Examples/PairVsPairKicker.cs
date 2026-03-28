namespace PokerHandComparer.Examples;

public class PairVsPairKicker
{
    public void Run()
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

        Console.WriteLine($"Hand 1: {pairAcesJackKicker}");
        Console.WriteLine($"Hand 2: {pairAcesTenKicker}");
        Console.WriteLine(HandComparer.GetResult(pairAcesJackKicker, pairAcesTenKicker));
    }
}