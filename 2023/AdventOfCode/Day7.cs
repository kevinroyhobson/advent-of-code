namespace AdventOfCode;

public class Day7
{
    private const string InputPath = "input/2023-12-07.txt";
    private List<string> _inputLines = File.ReadAllLines(InputPath).ToList();
    
    public long Puzzle1()
    {
        var hands = _inputLines.Select(inputLine => new CamelHand(inputLine))
                               .OrderBy(hand => hand.HandType)
                               .ThenBy(hand => hand.Cards[0].Value)
                               .ThenBy(hand => hand.Cards[1].Value)
                               .ThenBy(hand => hand.Cards[2].Value)
                               .ThenBy(hand => hand.Cards[3].Value)
                               .ThenBy(hand => hand.Cards[4].Value)
                               .ToList();

        int totalWinnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            int rank = i + 1;

            totalWinnings += hands[i].Bid * rank;
        }

        return totalWinnings;
    }

    private class CamelHand
    {
        public readonly List<CamelCard> Cards = new();
        
        public HandType HandType { get; }
        public int Bid { get; }   

        public CamelHand(string handInput)
        {
            string hand = handInput.Split()[0];
            var numCardsByValue = new Dictionary<int, int>();
            foreach (var cardLabel in hand)
            {
                var card = new CamelCard(cardLabel);
                Cards.Add(card);
                numCardsByValue[card.Value] = numCardsByValue.GetValueOrDefault(card.Value) + 1;
            }
            
            int maxMatchingCards = numCardsByValue.Values.Max();
            HandType = maxMatchingCards switch
            {
                5 => HandType.FiveOfAKind,
                4 => HandType.FourOfAKind,
                3 => numCardsByValue.Keys.Count == 2 ? HandType.FullHouse : HandType.ThreeOfAKind,
                2 => numCardsByValue.Keys.Count == 3 ? HandType.TwoPair : HandType.OnePair,
                _ => HandType.HighCard
            };
            
            Bid = Int32.Parse(handInput.Split()[1]);
        }
    }

    private class CamelCard
    {
        public int Value { get; }
        
        public CamelCard(char label)
        {
            Value = label switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                _ => label - '0'
            };    
        }
    }
    
    private enum HandType
    {
        HighCard = 1,
        OnePair = 2,
        TwoPair = 3,
        ThreeOfAKind = 4,
        FullHouse = 5,
        FourOfAKind = 6,
        FiveOfAKind = 7
    }
}
