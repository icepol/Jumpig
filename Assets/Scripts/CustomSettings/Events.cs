namespace pixelook
{
    public static class Events
    {
        public const string FLOOR_MOVE_FINISHED = "FloorMoveFinished";
        public const string PLAYER_JUMP_FINISHED = "PlayerJumpFinished";
        public const string PLAYER_JUMP_STARTED = "PlayerJumpStarted";
        public const string FLOOR_FALL_STARTED = "FloorFallStarted";
        public const string INIT_FLOOR_FINISHED = "InitFloorFinished";
        public const string SINGLE_TAP = "SingleTap";
        public const string DOUBLE_TAP = "DoubleTap";

        // level progression events
        public const string LEVEL_START = "LevelStart";
        public const string LEVEL_FINISHED = "LevelFinished";
        public const string LEVEL_FAILED = "LevelFailed";

        public const string PLAYER_DIED = "PlayerDied";

        public const string ENEMY_DIED = "EnemyDied";

        // transition events
        public const string TRANSITION_OPEN = "TransitionOpen";
        public const string TRANSITION_CLOSE = "TransitionClose";
        public const string TRANSITION_OPEN_FINISHED = "TransitionOpenFinished";
        public const string TRANSITION_CLOSE_FINISHED = "TransitionCloseFinished";

        // camera events 
        public const string BOUNDARIES_TOP_RIGHT = "BoundariesTopRight";
        public const string BOUNDARIES_BOTTOM_LEFT = "BoundariesBottomLeft";
        public const string CAMERA_START_FOLLOWING = "CameraStartFollowing";
        public const string CAMERA_STOP_FOLLOWING = "CameraStopFollowing";
        public const string CAMERA_SHAKE_BIG = "CameraShakeBig";
        public const string CAMERA_SHAKE_SMALL = "CameraShakeSmall";

        // settings events
        public const string MUSIC_SETTINGS_CHANGED = "MusicSettingsChanged";
    }
}