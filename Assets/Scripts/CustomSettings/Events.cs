namespace pixelook
{
    public static class Events
    {
        public const string FLOOR_MOVE_FINISHED = "FloorMoveFinished";
        public const string FLOOR_MOVE_STARTED = "FloorMoveStarted";
        public const string FLOOR_FALL_STARTED = "FloorFallStarted";
        public const string INIT_FLOOR_STARTED = "InitFloorStarted";
        public const string INIT_FLOOR_FINISHED = "InitFloorFinished";
        public const string FINISH_LINE_PASSED = "FinishLinePassed";
        
        public const string SINGLE_TAP = "SingleTap";
        public const string DOUBLE_TAP = "DoubleTap";

        // level progression events
        public const string GAME_STARTED = "GameStarted";
        public const string LEVEL_CHANGED = "LevelChanged";
        public const string SCORE_CHANGED = "ScoreChanged";

        public const string PLAYER_JUMP_FINISHED = "PlayerJumpFinished";
        public const string PLAYER_JUMP_STARTED = "PlayerJumpStarted";
        public const string PLAYER_MOVEMENT_FINISHED = "PlayerMovementFinished";
        public const string PLAYER_DIED = "PlayerDied";
        public const string PLAYER_COLLIDED_OBSTACLE = "PlayerCollidedObstacle";
        public const string PLAYER_FALLEN = "PlayerFallen";
        
        public const string COIN_COLLECTED = "CoinColled";
        public const string FOOD_COLLECTED = "FoodColled";

        // camera events
        public const string CAMERA_SHAKE_BIG = "CameraShakeBig";
        public const string CAMERA_SHAKE_SMALL = "CameraShakeSmall";
        
        // settings events
        public const string MUSIC_SETTINGS_CHANGED = "MusicSettingsChanged";
    }
}