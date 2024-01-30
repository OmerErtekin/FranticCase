using System;
using System.Collections.Generic;
using _Game.Scripts.Managers;
using UnityEngine;

namespace _Game.Scripts
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
    	#region Components
	    private Weapon _currentWeapon;
    	#endregion

    	#region Variables
	    [SerializeField] private List<Weapon> _weapons;
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

