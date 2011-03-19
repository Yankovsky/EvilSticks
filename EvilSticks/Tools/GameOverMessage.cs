using System;

namespace EvilSticks.Tools
{
    public static class GameOverMessage
    {
        static GameOverMessage()
        {
            _verbsCount = _verbs.GetUpperBound(0) - _verbs.GetLowerBound(0);
            _adjectivesCount = _adjectives.GetUpperBound(0) - _adjectives.GetLowerBound(0);
        }

        public static string GetRandom(string winnerName, string loserName)
        {
            return _adjectives[_random.Next(0, _adjectivesCount)] + " " + winnerName + " " + _verbs[_random.Next(0, _verbsCount)] + " " + loserName;
        }

        private static Random _random = new Random();
        private static string[] _adjectives = new string[] { "Heroic", "The Great", "Holy", "Brave", "Awesome", "Evil", "Furious", "Deadly", "Invincible", "Foxy", "Smart" };
        private static string[] _verbs = new string[] { "crushed", "crashed", "slashed", "smashed", "destroyed", "humiliated", "stopped", "beated", "rickrolled" };
        private static int _verbsCount;
        private static int _adjectivesCount;
    }
}