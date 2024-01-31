using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Controllers
{
    public class GameUIController : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private UpgradeUI _upgradeUI;
	    #endregion

	    #region Properties
	    public UpgradeUI UpgradeUI => _upgradeUI;
	    #endregion

	    private void Update()
	    {
		    if (Input.GetKeyDown(KeyCode.U))
		    {
			    _upgradeUI.ShowUpgradeUI();
		    }
	    }
    }
}

