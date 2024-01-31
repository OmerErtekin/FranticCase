using _Game.Scripts.Managers;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Components
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private PlayerWeaponHandler _weaponHandler;
        [SerializeField] private PlayerUpgradeHandler _upgradeHandler;
        [SerializeField] private Rigidbody _rigidbody;
        #endregion
        
        #region Propoerties
        public PlayerWeaponHandler WeaponHandler => _weaponHandler;
        public PlayerUpgradeHandler UpgradeHandler => _upgradeHandler;
        public PlayerAnimator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;
        public PlayerState PlayerState { get; set; }
        #endregion

        private void OnEnable()
        {
            EventManager.StartListening(EventManager.OnLevelInitialized,SetStateOnInitilaze);
            EventManager.StartListening(EventManager.OnPlayerStartToMove,SetStateOnMovement);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventManager.OnLevelInitialized,SetStateOnInitilaze);
            EventManager.StopListening(EventManager.OnPlayerStartToMove,SetStateOnMovement);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _upgradeHandler.Upgrade(UpgradeType.FireRate);
            }
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                _upgradeHandler.Upgrade(UpgradeType.BulletDamage);
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                _upgradeHandler.Upgrade(UpgradeType.AttackFormation);
            }
            
            if (Input.GetKeyDown(KeyCode.V))
            {
                _upgradeHandler.Upgrade(UpgradeType.BulletBounceCount);
            }
            
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _weaponHandler.EquipWeapon(WeaponTypes.Pistol);
                _weaponHandler.CurrentWeapon.CanFire = true;
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                _weaponHandler.EquipWeapon(WeaponTypes.SmgRifle);
                _weaponHandler.CurrentWeapon.CanFire = true;
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                _weaponHandler.EquipWeapon(WeaponTypes.Taser);
                _weaponHandler.CurrentWeapon.CanFire = true;
            }
        }

        public void WinTheLevel()
        {
            if(PlayerState == PlayerState.Won) return;
            
            PlayerState = PlayerState.Won;
            _mover.StopMovement(PlayerAnims.Victory);
            EventManager.TriggerEvent(EventManager.OnLevelCompleted);
        }
        
        public void FailTheLevel()
        {
            if(PlayerState == PlayerState.Failed) return;
            
            PlayerState = PlayerState.Failed;
            _mover.StopMovement(PlayerAnims.Fail);
            EventManager.TriggerEvent(EventManager.OnLevelFailed);
        }

        private void SetStateOnInitilaze() => PlayerState = PlayerState.WaitForStart;

        private void SetStateOnMovement() => PlayerState = PlayerState.Run;
    }
}

