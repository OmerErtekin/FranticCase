using System;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private PlayerMover _mover;
	    [SerializeField] private PlayerAnimator _animator;
	    [SerializeField] private PlayerWeaponHandler _weaponHandler;
	    [SerializeField] private Rigidbody _rigidbody;
	    #endregion

	    #region Variables
	    public PlayerMover Mover => _mover;
	    public PlayerAnimator Animator => _animator;
	    public PlayerWeaponHandler WeaponHandler => _weaponHandler;
	    public Rigidbody Rigidbody => _rigidbody;
	    #endregion

	    private void Update()
	    {
		    if (Input.GetKeyDown(KeyCode.F1))
		    {
			    WeaponHandler.EquipWeapon(WeaponTypes.Pistol);
		    }
		    
		    if (Input.GetKeyDown(KeyCode.F2))
		    {
			    WeaponHandler.EquipWeapon(WeaponTypes.SmgRifle);
		    }
		    
		    if (Input.GetKeyDown(KeyCode.F3))
		    {
			    WeaponHandler.EquipWeapon(WeaponTypes.Taser);
		    }
	    }
    }
}

