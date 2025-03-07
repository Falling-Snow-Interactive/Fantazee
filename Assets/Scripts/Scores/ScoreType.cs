namespace Fantazee.Scores
{
    public enum ScoreType
    {
        None = 0, 
        
        // Best
        Fantazee = 1,
        
        // Numbers
        Ones = 2,
        Twos = 3,
        Threes = 4,
        Fours = 5,
        Fives = 6,
        Sixes = 7,
        
        // Matches
        TwoOfAKind = 8,
        ThreeOfAKind = 9,
        FourOfAKind = 10,
        FiveOfAKind = 11,
        TwoPair = 12,
        FullHouse = 13,
        
        // Runs
        SmallRun = 14,
        LargeRun = 15,
        FullRun = 16,
        
        // Even/Odds
        Evens = 17,
        Odds = 18,
        
        // Other
        Chance = 19,
    }
}