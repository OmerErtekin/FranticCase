using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts;
using _Game.Scripts.Controllers;
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
            if(!IsUpgradeAvailable(upgradeType)) return;
            
            switch (upgradeType)
            {
                case UpgradeType.FireRate:
                    UpgradeData.FireRateLevel++;
                    break;
                case UpgradeType.BulletDamage:
                    UpgradeData.BulletDamageLevel++;
                    break;
                case UpgradeType.AttackFormation:
                    UpgradeData.AttackFormation = (AttackFormation)((int)UpgradeData.AttackFormation + 1);
                    break;
                case UpgradeType.BulletBounceCount:
                    UpgradeData.BulletBounceCount++;
                    break;
            }
            EventManager.TriggerEvent(EventManager.OnPlayerUpgraded);
        }

        public List<UpgradeType> GetAvailableUpgrades()
        {
            return Enum.GetValues(typeof(UpgradeType)).Cast<UpgradeType>().Where(IsUpgradeAvailable)
                .ToList();
        }
        
        private bool IsUpgradeAvailable(UpgradeType upgradeType)
        {
            return upgradeType switch
            {
                UpgradeType.FireRate => UpgradeData.FireRateLevel < Constants.MAX_FIRE_RATE_LEVEL,
                UpgradeType.BulletDamage => UpgradeData.BulletDamageLevel < Constants.MAX_DAMAGE_LEVEL,
                UpgradeType.AttackFormation => (int)UpgradeData.AttackFormation < Constants.MAX_FORMATION_LEVEL,
                UpgradeType.BulletBounceCount => UpgradeData.BulletBounceCount < Constants.MAX_BOUNCE_BULLET_LEVEL,
                _ => false
            };
        }
    }
}

[Serializable]
public class PlayerUpgradeData
{
    public int FireRateLevel;
    public int BulletDamageLevel;
    public AttackFormation AttackFormation;
    public int BulletBounceCount;
}