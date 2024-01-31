namespace _Game.Scripts
{
    public static class Constants
    {
        #region Weapon Constants
        public const int SINGLE_DIAGONAL_START_INDEX = 0;
        public const int MULTI_DIAGONAL_START_INDEX = 3;
        #endregion
        
        #region Movement Constants
        public const float MOVEMENT_SPEED = 5;
        public const float MAX_SWERVE_AMOUNT = 2.5f;
        public const float SWERVE_SPEED = 2;
        public const float MAP_WIDTH = 5;
        public const float X_VELOCITY_TRESHHOLD = 0.5f;
        #endregion

        #region Upgrade Constants
        public const int MAX_FIRE_RATE_LEVEL = 2;
        public const int MAX_DAMAGE_LEVEL = 2;
        public const int MAX_FORMATION_LEVEL = 2;
        public const int MAX_BOUNCE_BULLET_LEVEL = 3;
        public const int TOTAL_COUNT_OF_UPGRADE = 7;
        #endregion

        #region Level Constants
        public const float UPGRADE_START_PERCENTAGE = 0.05f;
        public const float UPGRADE_END_PERCENTAGE = 0.8f;
        #endregion

        #region Player Constants
        public static string KEY_PLAYER_LEVEL = "PlayerLevel";
        #endregion
    }
}
