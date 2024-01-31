using _Game.Scripts.Controllers;
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

        #region Variables
        private PlayerState _playerState;
        #endregion
        
        #region Propoerties
        public PlayerWeaponHandler WeaponHandler => _weaponHandler;
        public PlayerUpgradeHandler UpgradeHandler => _upgradeHandler;
        public PlayerAnimator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;
        #endregion

        private void OnEnable()
        {
            EventManager.StartListening(EventManager.OnLevelInitialized, ()=> _playerState = PlayerState.WaitForStart);
            EventManager.StartListening(EventManager.OnLevelStarted, ()=> _playerState = PlayerState.Run);
        }

        public void WinTheLevel()
        {
            if(_playerState == PlayerState.Won) return;
            
            _playerState = PlayerState.Won;
            _mover.StopMovement(PlayerAnims.Victory);
            EventManager.TriggerEvent(EventManager.OnLevelCompleted);
        }
        
        public void FailTheLevel()
        {
            if(_playerState == PlayerState.Failed) return;
            
            _playerState = PlayerState.Failed;
            _mover.StopMovement(PlayerAnims.Fail);
            EventManager.TriggerEvent(EventManager.OnLevelFailed);
        }
    }
}

