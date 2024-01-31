using System.Collections.Generic;
using _Game.Scripts.Controllers;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts
{
    public class Weapon : MonoBehaviour
    {
        #region Components
        private PlayerAnimator _playerAnimator;
        private ObjectPoolController _objectPool;
        private Bullet _currentBullet;
        #endregion

        #region Variables
        //Each weapon has different scaling with base upgrade factor as you suggested.
        [SerializeField] private float _baseFireRate, _baseUpgradeFactor;
        [SerializeField] private int _baseBulletDamage;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private Transform _leftHandTransform, _aimTransform;
        [SerializeField] private WeaponTypes _type;
        private List<Transform> _firePoses;
        private float _currentFireRate, _nextFireTime;
        private int _currentBulletDamage, _currentBulletBounceCount;
        private AttackFormation _currentFormation;
        #endregion

        #region Properties
        public Transform LeftHandTargetTransform => _leftHandTransform;
        public Transform AimTargetTransform => _aimTransform;
        public WeaponTypes WeaponType => _type;
        public bool CanFire { private get; set; }
        #endregion

        private void OnEnable()
        {
            EventManager.StartListening(EventManager.OnPlayerUpgraded, ApplyUpgrades);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventManager.OnPlayerUpgraded, ApplyUpgrades);
        }

        public void EnableWeapon()
        {
            _objectPool = GameController.Instance.ObjectPool;
            _playerAnimator = GameController.Instance.Player.Animator;
            _firePoses = GameController.Instance.Player.WeaponHandler.FirePoses;
            ApplyUpgrades();
            gameObject.SetActive(true);
        }

        public void DisableWeapon()
        {
            CanFire = false;
            gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            Fire();
        }

        private void ApplyUpgrades()
        {
            //I use upgradeData.XLevel - 1 because levels are starting from 1
            var upgradeData = GameController.Instance.Player.UpgradeHandler.UpgradeData;
            _currentBulletDamage = _baseBulletDamage + (int)(1 * (upgradeData.BulletDamageLevel - 1) * _baseUpgradeFactor);
            _currentFireRate = _baseFireRate + 1 * (upgradeData.FireRateLevel - 1) * _baseUpgradeFactor;
            _currentFormation = upgradeData.AttackFormation;
            _currentBulletBounceCount = upgradeData.BulletBounceCount;
        }

        private void Fire()
        {
            if (!_playerAnimator.IsIKEnabled || !CanFire || Time.time < _nextFireTime) return;

            _nextFireTime = Time.time + 1 / _currentFireRate;
            //I tried to have scalable implementation, if we want to add some more formation.
            //In this approach, all we have to do is add new position indexes at firePoses on PlayerWeaponHandler.cs and 
            //define an start-end index.
            switch (_currentFormation)
            {
                case AttackFormation.Single:
                    _currentBullet = _objectPool.GetBullet();
                    _currentBullet.SetBullet(_currentBulletDamage, _currentBulletBounceCount, _bulletSpeed, _firePoses[0], _type);
                    break;
                case AttackFormation.SingleDiagonal:
                    for (var i = Constants.SINGLE_DIAGONAL_START_INDEX; i < Constants.SINGLE_DIAGONAL_START_INDEX + 3; i++)
                    {
                        _currentBullet = _objectPool.GetBullet();
                        _currentBullet.SetBullet(_currentBulletDamage, _currentBulletBounceCount, _bulletSpeed, _firePoses[i], _type);
                    }
                    break;
                case AttackFormation.MultipleDiagonal:
                    for (var i = Constants.MULTI_DIAGONAL_START_INDEX; i < Constants.MULTI_DIAGONAL_START_INDEX + 6; i++)
                    {
                        _currentBullet = _objectPool.GetBullet();
                        _currentBullet.SetBullet(_currentBulletDamage, _currentBulletBounceCount, _bulletSpeed, _firePoses[i], _type);
                    }
                    break;
                /*Psedeu implementation of how we can add a new formation
                 case AttackFormation.NewFormation
                    for (var i = Constants.NEW_START_INDEX; i < Constants.NEW_START_INDEX + BULLET_COUNT; i++)
                    {
                        _currentBullet = _objectPool.GetBullet();
                        _currentBullet.SetBullet(_currentBulletDamage, _currentBulletBounceCount, _bulletSpeed, _firePoses[i], _type);
                    }
                    break;
                 */
            }
        }
    }
}