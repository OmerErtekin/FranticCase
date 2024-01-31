using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Controllers
{
    public class GameUIController : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private UpgradeUI _upgradeUI;
	    [SerializeField] private FadeableUI _tapToStartPanel,_levelFailPanel, _levelCompletedPanel;
	    #endregion

	    #region Properties
	    public UpgradeUI UpgradeUI => _upgradeUI;
	    #endregion

	    private void OnEnable()
	    {
		    EventManager.StartListening(EventManager.OnLevelCompleted, ()=> _levelCompletedPanel.FadeIn(0.5f,delay:2));
		    EventManager.StartListening(EventManager.OnLevelFailed,()=> _levelFailPanel.FadeIn(0.5f,delay:2));
		    EventManager.StartListening(EventManager.OnLevelInitialized,()=> _tapToStartPanel.FadeIn(0.5f));
	    }

	    public void OnTapToPlayClicked()
	    {
		    _tapToStartPanel.FadeOutDirectly();
		    EventManager.TriggerEvent(EventManager.OnLevelStarted);
	    }
	    
	    public void OnNextLevelClicked()
	    {
		    GameController.Instance.LevelController.PassNextLevel();
		    _levelCompletedPanel.FadeOut(0.5f);
	    }

	    public void OnRetryLevelClicked()
	    {
		    GameController.Instance.LevelController.RetryCurrentLevel();
		    _levelFailPanel.FadeOut(0.5f);
	    }
    }
}

