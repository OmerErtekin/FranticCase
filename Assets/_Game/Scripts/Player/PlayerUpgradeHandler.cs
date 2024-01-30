using System;
using _Game.Scripts;
using _Game.Scripts.Managers;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerUpgradeHandler : MonoBehaviour
    {
        #region Properties
        public PlayerUpgradeData UpgradeData { get; private set; }
        #endregion

        private void OnEnable()
        {
            EventManager.StartListening(EventManager.OnLevelInitialized, InitializeUpgradeData);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventManager.OnLevelInitialized, InitializeUpgradeData);
        }

        private void Awake()
        {
            InitializeUpgradeData();
        }

        private void InitializeUpgradeData()
        {
            UpgradeData = new PlayerUpgradeData
            {
                FireRateLevel = 1,
                BulletDamageLevel = 1,
                AttackFormation = AttackFormation.Single,
                BulletBounceCount = 0
            };
        }
        
        public void Upgrade(UpgradeType upgradeType)
        {
            switch (upgradeType)
            {
                case UpgradeType.FireRate:
                    if (UpgradeData.FireRateLevel >= Constants.MAX_FIRE_RATE_LEVEL) return;
                    
                    UpgradeData.FireRateLevel++;
                    break;
                case UpgradeType.BulletDamage:
                    if (UpgradeData.BulletDamageLevel >= Constants.MAX_DAMAGE_LEVEL) return;
                    
                    UpgradeData.BulletDamageLevel++;
                    break;
                case UpgradeType.AttackFormation:
                    if ((int)UpgradeData.AttackFormation >= Constants.MAX_FORMATION_LEVEL) return;
                    
                    UpgradeData.AttackFormation = (AttackFormation)((int)UpgradeData.AttackFormation + 1);
                    break;
                case UpgradeType.BulletBounceCount:
                    if (UpgradeData.BulletBounceCount >= Constants.MAX_BOUNCE_BULLET_LEVEL) return;
                    
                    UpgradeData.BulletBounceCount++;
                    break;
            }

            EventManager.TriggerEvent(EventManager.OnPlayerUpgraded);
        }
    }
}

[System.Serializable]
public class PlayerUpgradeData
{
    public int FireRateLevel;
    public int BulletDamageLevel;
    public AttackFormation AttackFormation;
    public int BulletBounceCount;
}