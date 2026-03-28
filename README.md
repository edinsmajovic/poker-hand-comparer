# Poker Hand Comparer

A C# console application that evaluates and compares two 5-card poker hands, determining the winner based on standard poker hand rankings and tie-breaking rules.

## Project Structure

```
poker-hand-comparer/
├── src/
│   └── PokerHandComparer/
│       ├── Examples/
│       │   ├── ExampleRunner.cs
│       │   ├── FlushVsStraight.cs
│       │   ├── FullHouseVsFlush.cs
│       │   └── PairVsPairKicker.cs
│       ├── Card.cs
│       ├── CardSuit.cs
│       ├── CardValue.cs
│       ├── Hand.cs
│       ├── HandComparer.cs
│       ├── HandEvaluator.cs
│       ├── HandRank.cs
│       └── Program.cs
└── tests/
    └── PokerHandComparer.Tests/
        ├── HandComparerTests.cs
        └── HandEvaluatorTests.cs
```

## Hand Rankings

From lowest to highest:

| Rank | Name |
|------|------|
| 1 | High Card |
| 2 | Pair |
| 3 | Two Pairs |
| 4 | Three of a Kind |
| 5 | Straight |
| 6 | Flush |
| 7 | Full House |
| 8 | Four of a Kind |
| 9 | Straight Flush |

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Run the examples

```bash
dotnet run --project src/PokerHandComparer
```

### Run the tests

```bash
dotnet test
```

## Usage

```csharp
var hand1 = new Hand(new[]
{
    new Card(CardSuit.H, CardValue.Two),
    new Card(CardSuit.H, CardValue.Five),
    new Card(CardSuit.H, CardValue.Seven),
    new Card(CardSuit.H, CardValue.Nine),
    new Card(CardSuit.H, CardValue.Jack)
});

var hand2 = new Hand(new[]
{
    new Card(CardSuit.C, CardValue.Six),
    new Card(CardSuit.D, CardValue.Seven),
    new Card(CardSuit.H, CardValue.Eight),
    new Card(CardSuit.S, CardValue.Nine),
    new Card(CardSuit.C, CardValue.Ten)
});

Console.WriteLine(HandComparer.GetResult(hand1, hand2));
```

Output:
```
Hand 1 has won!
```

### `HandComparer` API

| Method | Returns | Description |
|--------|---------|-------------|
| `Compare(hand1, hand2)` | `int` | `1` if hand1 wins, `-1` if hand2 wins, `0` for a tie |
| `GetResult(hand1, hand2)` | `string` | Human-readable result string |

## Suits

| Value | Suit |
|-------|------|
| `C` | Clubs |
| `D` | Diamonds |
| `H` | Hearts |
| `S` | Spades |