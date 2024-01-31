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
	    #endregion

	    private void OnEnable()
	    {
		    EventManager.StartListening(EventManager.OnLevelInitialized, () => EquipWeapon(WeaponTypes.Pistol,false));
		    EventManager.StartListening(EventManager.OnLevelStarted,StartFiringAtLevelStart);
		    EventManager.StartListening(EventManager.OnLevelCompleted,StopFiringAtLevelEnd);
		    EventManager.StartListening(EventManager.OnLevelFailed,StopFiringAtLevelEnd);
	    }

	    private void OnDisable()
	    {
		    EventManager.StopListening(EventManager.OnLevelStarted,StartFiringAtLevelStart);
		    EventManager.StopListening(EventManager.OnLevelCompleted,StopFiringAtLevelEnd);
		    EventManager.StopListening(EventManager.OnLevelFailed,StopFiringAtLevelEnd);
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

	    public void EquipWeapon(WeaponTypes type,bool canFire)
	    {
		    foreach (var weapon in _weapons)
		    {
			    if (weapon.WeaponType == type)
			    {
				    _currentWeapon = weapon;
				    _currentWeapon.CanFire = canFire;
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

