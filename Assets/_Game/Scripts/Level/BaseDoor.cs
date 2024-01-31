using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.Level
{
    public abstract class BaseDoor : MonoBehaviour
    {
	    #region Variables
	    [SerializeField] private GameObject _activeObjectsParent;
	    private bool _isPerformed;
	    #endregion
	    
	    protected virtual void OnTriggerEnter(Collider other)
	    {
		    if(_isPerformed || !other.TryGetComponent(out PlayerController player)) return;
		    
		    OnContactedWithPlayer(player);
	    }
	    
	    public virtual void ResetDoor()
	    {
		    if (_activeObjectsParent)
		    {
			    _activeObjectsParent.SetActive(true);
		    }
		    _isPerformed = false;
	    }

	    protected virtual void OnContactedWithPlayer(PlayerController player)
	    {
		    if (_activeObjectsParent)
		    {
			    _activeObjectsParent.SetActive(false);
		    }
		    _isPerformed = true;
	    }
    }
}

