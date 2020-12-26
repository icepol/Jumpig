namespace pixelook
{
    public static class GameState
    {
        private static int _score;

        public static int Score
        {
            get => _score;
            
            set
            {
                _score = value;
                EventManager.TriggerEvent(Events.SCORE_CHANGED);
            }
        }
        
        public static int SpawnedRowsCount { get; set; }

        public static void Reset()
        {
            _score = 0;
        }
    }
}