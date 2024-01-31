using _Game.Scripts.Controllers;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerMover : MonoBehaviour
    {
    	#region Components
	    private PlayerAnimator _animator;
	    private PlayerController _controller;
	    private Rigidbody _rigidbody;
    	#endregion

    	#region Variables
	    private bool _canMove;
	    private float _inputX, _zSpeed, _lerpedX;
	    private Vector3 _lastMousePos;
        #endregion

        private void OnEnable()
        {
	        EventManager.StartListening(EventManager.OnLevelStarted,StartMovement);
        }

        private void OnDisable()
        {
	        EventManager.StopListening(EventManager.OnLevelStarted,StartMovement);
        }

        private void Awake()
        {
	        _controller = GameController.Instance.Player;
	        _rigidbody = _controller.Rigidbody;
	        _animator = _controller.Animator;
        }

        private void Update()
        {
            Move();
	        if (!_canMove) return;
	        
	        HandleInput();
	        DecideRunAnimation();
        }
        
        private void Move()
        {
	        _inputX = Mathf.Clamp(_inputX, -Constants.MAX_SWERVE_AMOUNT, Constants.MAX_SWERVE_AMOUNT) * Constants.SWERVE_SPEED;
	        _lerpedX = Mathf.Lerp(_lerpedX, _inputX, 25 * Time.deltaTime);
	        if ((transform.position.x > Constants.MAP_WIDTH / 2 && _lerpedX > 0) || (transform.position.x < -Constants.MAP_WIDTH / 2 && _lerpedX < 0))
	        {
		        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, _zSpeed);
	        }
	        else
	        {
		        _rigidbody.velocity = new Vector3(_lerpedX, _rigidbody.velocity.y, _zSpeed);
	        }
        }

        private void HandleInput()
        {
	        if (Input.GetMouseButtonDown(0))
	        {
		        _lastMousePos = Input.mousePosition;
	        }
	        else if (Input.GetMouseButton(0))
	        {
		        _inputX = Input.mousePosition.x - _lastMousePos.x;
		        _lastMousePos = Input.mousePosition;
	        }
	        else if (Input.GetMouseButtonUp(0))
	        {
		        _inputX = 0;
	        }
        }

        private void DecideRunAnimation()
        {
	        if (Mathf.Abs(_rigidbody.velocity.x) < Constants.X_VELOCITY_TRESHHOLD)
	        {
		        _animator.PlayAnimation(PlayerAnims.RunForward,0.25f);
	        }
	        else if (_rigidbody.velocity.x < -Constants.X_VELOCITY_TRESHHOLD)
	        {
		        _animator.PlayAnimation(PlayerAnims.RunLeft);
	        }
	        else
	        {
		        _animator.PlayAnimation(PlayerAnims.RunRight);
	        }
        }
        
        private void StartMovement()
        {
	        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
	        _zSpeed = Constants.MOVEMENT_SPEED;
	        _canMove = true;
	        EventManager.TriggerEvent(EventManager.OnPlayerStartToMove);
        }

        public void StopMovement(PlayerAnims animOnStop)
        {
	        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	        _canMove = false;
	        _inputX = 0;
	        _zSpeed = 0;
	        _animator.PlayAnimation(animOnStop);
	        EventManager.TriggerEvent(EventManager.OnPlayerStop);
        }
    }
}

