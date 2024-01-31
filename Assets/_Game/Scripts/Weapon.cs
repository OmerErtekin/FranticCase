using System.Collections.Generic;
using _Game.Scripts.Managers;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts
{
    public class Weapon : MonoBehaviour
    {
        #region Components
        private PlayerAnimator _playerAnimator;
        private ObjectPoolManager _objectPool;
        private Bullet _currentBullet;
        #endregion

        #region Variables
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
            }
        }
    }
}