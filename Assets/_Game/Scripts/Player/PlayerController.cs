using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private PlayerMover _mover;
	    [SerializeField] private PlayerAnimator _animator;
	    [SerializeField] private Rigidbody _rigidbody;
	    #endregion

	    #region Variables
	    public PlayerMover Mover => _mover;
	    public PlayerAnimator Animator => _animator;
	    public Rigidbody Rigidbody => _rigidbody;
	    #endregion
    }
}

