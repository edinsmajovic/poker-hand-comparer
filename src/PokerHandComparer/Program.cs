using PokerHandComparer;

// Example: Compare two poker hands
var hand1 = new Hand(new List<Card>
{
    new(CardSuit.H, CardValue.Ten),
    new(CardSuit.H, CardValue.Jack),
    new(CardSuit.H, CardValue.Queen),
    new(CardSuit.H, CardValue.King),
    new(CardSuit.H, CardValue.Ace)
});

var hand2 = new Hand(new List<Card>
{
    new(CardSuit.C, CardValue.Ten),
    new(CardSuit.D, CardValue.Ten),
    new(CardSuit.S, CardValue.Ten),
    new(CardSuit.H, CardValue.Ten),
    new(CardSuit.C, CardValue.Ace)
});

Console.WriteLine($"Hand 1: {hand1}");
Console.WriteLine($"Hand 2: {hand2}");

var result = hand1.CompareTo(hand2);

if (result > 0)
    Console.WriteLine("Hand 1 wins!");
else if (result < 0)
    Console.WriteLine("Hand 2 wins!");
else
    Console.WriteLine("It's a tie!");
