using System.Collections.Generic;
using _Game.Scripts.Controllers;
using UnityEngine;

namespace _Game.Scripts
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private Follower _firePosFollower;
	    private Weapon _currentWeapon;
    	#endregion

    	#region Variables
	    //To have easier configurations on fire poses, i made them with transforms. So anyone can just move transforms as s/he wish.
	    [SerializeField] private List<Transform> _firePoses;
	    [SerializeField] private List<Weapon> _weapons;
	    #endregion

	    #region Properties
	    public List<Transform> FirePoses => _firePoses;
	    public Weapon CurrentWeapon => _currentWeapon;
	    #endregion

	    private void OnEnable()
	    {
		    EventManager.StartListening(EventManager.OnLevelInitialized,EquipPistolAtLevelInit);
		    EventManager.StartListening(EventManager.OnLevelStarted,StartFiringAtLevelStart);
		    EventManager.StartListening(EventManager.OnLevelCompleted,StopFiringAtLevelEnd);
		    EventManager.StartListening(EventManager.OnLevelFailed,StopFiringAtLevelEnd);
	    }

	    private void OnDisable()
	    {
		    EventManager.StopListening(EventManager.OnLevelInitialized,EquipPistolAtLevelInit);
		    EventManager.StopListening(EventManager.OnLevelStarted,StartFiringAtLevelStart);
		    EventManager.StopListening(EventManager.OnLevelCompleted,StopFiringAtLevelEnd);
		    EventManager.StopListening(EventManager.OnLevelFailed,StopFiringAtLevelEnd);
	    }

	    private void EquipPistolAtLevelInit()
	    {
		    EquipWeapon(WeaponTypes.Pistol);
		    _currentWeapon.CanFire = false;
	    }

	    private void StartFiringAtLevelStart()
	    {
		    if(!_currentWeapon) return;
            
		    _currentWeapon.CanFire = true;
	    }

	    private void StopFiringAtLevelEnd()
	    {
		    if(!_currentWeapon) return;
		    
		    _currentWeapon.CanFire = false;
	    }

	    public void EquipWeapon(WeaponTypes type)
	    {
		    if(_currentWeapon && _currentWeapon.WeaponType == type) return;
		    
		    foreach (var weapon in _weapons)
		    {
			    if (weapon.WeaponType == type)
			    {
				    _currentWeapon = weapon;
				    _firePosFollower.SetTarget(_currentWeapon.AimTargetTransform);
				    weapon.EnableWeapon();
			    }
			    else
			    {
				    weapon.DisableWeapon();
			    }
		    }
		    
		    EventManager.TriggerEvent(EventManager.OnWeaponEquiped,_currentWeapon);
	    }
    }
}

