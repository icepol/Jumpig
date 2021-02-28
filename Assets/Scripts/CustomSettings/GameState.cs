namespace pixelook
{
    public static class GameState
    {
        private static int _score;

        private static int _level = 1;

        public static int Score
        {
            get => _score;
            
            set
            {
                _score = value;
                EventManager.TriggerEvent(Events.SCORE_CHANGED);
            }
        }
        
        public static int Coins { get; set; }

        public static int Distance { get; set; }

        public static int SpawnedRowsCount { get; set; }

        public static int Level
        {
            get => _level;

            set
            {
                _level = value;
                EventManager.TriggerEvent(Events.LEVEL_CHANGED);
            }
        }
        
        public static int ComboMultiplier { get; set; }

        public static void Reset()
        {
            _score = 0;
            Coins = 0;
            Distance = 0;
            Level = 1;
            ComboMultiplier = 0;
        }
    }
}