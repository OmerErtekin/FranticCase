using System.Collections.Generic;
using _Game.Scripts.Controllers;
using _Game.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class UpgradeCard : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private TMP_Text _upgradeText;
	    [SerializeField] private Image _upgradeImage;
	    [SerializeField] private FadeableUI _fadeableUI;
    	#endregion

    	#region Variables
	    [SerializeField] private List<string> _upgradeTexts;
	    [SerializeField] private List<Sprite> _upgradeSprites;
	    private UpgradeType _upgradeType;
	    private bool _isPerformedUpgrade;
        #endregion

        public void ShowUpgradeCard(UpgradeType type)
        {
	        _isPerformedUpgrade = false;
	        _upgradeType = type;
	        _upgradeText.text = _upgradeTexts[(int)type];
	        _upgradeImage.sprite = _upgradeSprites[(int)type];
	        _fadeableUI.FadeIn(0.25f);
        }
        
        public void HideUpgradeCardDirectly()
        {
	        _fadeableUI.FadeOutDirectly();
        }

        public void OnUpgradeClicked()
        {
	        if(_isPerformedUpgrade) return;

	        _isPerformedUpgrade = true;
            GameController.Instance.Player.UpgradeHandler.Upgrade(_upgradeType);
            GameController.Instance.UIController.UpgradeUI.HideUpgradeUI();
        }
    }
}

