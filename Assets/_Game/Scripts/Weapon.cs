using UnityEngine;

namespace _Game.Scripts
{
    public class Weapon : MonoBehaviour
    {
		#region Variables
	    [SerializeField] private float _baseFireRate, _baseBulletTravelSpeed;
	    [SerializeField] private int _baseBulletDamage;
	    [SerializeField] private Transform _leftHandTransform, _aimTransform;
	    [SerializeField] private WeaponTypes _type;
	    public bool CanFire { get; set; }
	    #endregion

	    #region Properties
	    public Transform leftHandTargetTransform => _leftHandTransform;
	    public Transform aimTargetTransform => _aimTransform;
	    public WeaponTypes WeaponType => _type;
	    #endregion

	    public void EnableWeapon()
	    {
		    gameObject.SetActive(true);
	    }

	    public void DisableWeapon()
	    {
		    CanFire = false;
		    gameObject.SetActive(false);
	    }
    }
}

