using System;

namespace EvilSticks.Tools
{
    public static class BotNames
    {
        static BotNames()
        {
            _namesCount = _names.GetUpperBound(0) - _names.GetLowerBound(0);
            _modificatorsCount = _modificators.GetUpperBound(0) - _modificators.GetLowerBound(0);
        }

        public static string GetRandom()
        {
            return _names[_random.Next(0, _namesCount)] + _modificators[_random.Next(0, _modificatorsCount)];
        }

        private static Random _random = new Random();
        private static string[] _names = new string[] { "IVAN", "MedvePut", "Obama", "Terminator", "Anfisa", "Agata", "Lada", "Avdotya", "Stepa" };
        private static string[] _modificators = new string[] { "-2000", "~Me", " XP", " 4G", ".NET", ".ru", "7", "1.1", " №1", "(C)", "+", "666", "777", ".AI", ".Bot" };
        private static int _namesCount;
        private static int _modificatorsCount;
    }
}