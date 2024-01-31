using System.Collections.Generic;
using _Game.Scripts.Level;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts
{
    public class WeaponDoor : BaseDoor
    {
    	#region Components
    	#endregion

    	#region Variables
	    [SerializeField] private List<GameObject> _weaponModels;
	    [SerializeField] private WeaponTypes _type;
	    #endregion

	    private void OnEnable()
	    {
		    for (var i = 0; i < _weaponModels.Count; i++)
		    {
			    _weaponModels[i].SetActive(i == (int)_type);
		    }
	    }

	    protected override void OnContactedWithPlayer(PlayerController player)
	    {
		    base.OnContactedWithPlayer(player);
		    player.WeaponHandler.EquipWeapon(_type,true);
	    }
    }
}

