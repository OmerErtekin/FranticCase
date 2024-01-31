using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Controllers;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class UpgradeUI : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private FadeableUI _fadeableUI;
	    #endregion

	    #region Variables
	    [SerializeField] private List<UpgradeCard> _upgradeCards;
	    private List<UpgradeType> _availableUpgrades = new();
	    #endregion

	    public void ShowUpgradeUI()
	    {
		    _availableUpgrades = GameController.Instance.Player.UpgradeHandler.GetAvailableUpgrades();
		    if(_availableUpgrades.Count == 0) return;
		    
		    Time.timeScale = 0;
		    _fadeableUI.FadeIn(0.25f);
		    _availableUpgrades.Shuffle();
		    if (_availableUpgrades.Count > 3)
		    {
			    _availableUpgrades.RemoveRange(3,_availableUpgrades.Count - 3);
		    }

		    StartCoroutine(ShowUpgradesRoutine());
	    }

	    public void HideUpgradeUI()
	    {
		    Time.timeScale = 1;
		    _fadeableUI.FadeOut(0.25f,onFadeCompleted: () =>
		    {
			    foreach (var card in _upgradeCards)
			    {
				    card.HideUpgradeCardDirectly();
			    }
		    });
	    }

	    private IEnumerator ShowUpgradesRoutine()
	    {
		    for (var i = 0; i < _availableUpgrades.Count; i++)
		    {
			    _upgradeCards[i].gameObject.SetActive(true);
		    }
		    
		    for (var i = 0; i < _availableUpgrades.Count; i++)
		    {
			    _upgradeCards[i].ShowUpgradeCard(_availableUpgrades[i]);
			    yield return new WaitForSecondsRealtime(0.25f);
		    }
	    }
    }
}

