namespace _Game.Scripts
{
    public static class Constants
    {
        #region Weapon Constants
        public const float ANGLE_BETWEEN_FIRE_POSES = 20;
        public const float DISTANCE_BETWEEN_FIRE_POSES = 2;
        #endregion
        
        #region Movement Constants
        public const float MOVEMENT_SPEED = 5;
        public const float MAX_SWERVE_AMOUNT = 5;
        public const float SWERVE_SPEED = 5;
        public const float MAP_WIDTH = 5;
        public const float X_VELOCITY_TRESHHOLD = 0.05f;
        #endregion

        #region Upgrade Constants
        public const int MAX_FIRE_RATE_LEVEL = 3;
        public const int MAX_DAMAGE_LEVEL = 3;
        public const int MAX_FORMATION_LEVEL = 2;
        public const int MAX_BOUNCE_BULLET_LEVEL = 3;
        #endregion
    }
}
