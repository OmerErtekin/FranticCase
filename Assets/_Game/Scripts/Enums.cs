namespace _Game.Scripts
{
    public enum PlayerAnims
    {
        Idle = 0,
        RunForward = 1,
        RunRight = 2,
        RunLeft = 3,
        Fail = 4,
        Victory = 5
    }

    public enum WeaponTypes
    {
        Pistol = 0,
        SmgRifle = 1,
        Taser = 2
    }

    public enum AttackFormation
    {
        Single,
        SingleDiagonal,
        MultipleDiagonal,
    }
    
    public enum UpgradeType
    {
        FireRate,
        BulletDamage,
        AttackFormation,
        BulletBounceCount
    }
    
    [System.Flags]
    public enum Axis
    {
        None = 0,
        X = 1,
        Y = 2,
        Z = 4 
    }
}

