using System.Collections.Generic;
using _Game.Scripts.Level;
using UnityEngine;

namespace _Game.Scripts.Controllers
{
    public class LevelController : MonoBehaviour
    {
		#region Variables
	    [SerializeField] private List<LevelParent> _gameLevels;
	    private int _currentLevel;
        #endregion

        private void Start()
        {
	        _currentLevel = PlayerPrefs.GetInt(Constants.KEY_PLAYER_LEVEL, 0);
	        _gameLevels[_currentLevel].InitilazeLevel();
        }

        private void Update()
        {
	        if (Input.GetKeyDown(KeyCode.S))
	        {
		        EventManager.TriggerEvent(EventManager.OnLevelStarted);
	        }
	        
	        if (Input.GetKeyDown(KeyCode.D))
	        {
		        _gameLevels[0].InitilazeLevel();
	        }
        }

        public void PassNextLevel()
        {
	        _gameLevels[_currentLevel].CloseLevel();
	        _currentLevel = PlayerPrefs.GetInt(Constants.KEY_PLAYER_LEVEL, 0);
	        _currentLevel++;
	        if (_currentLevel >= _gameLevels.Count)
	        {
		        _currentLevel = 0;
	        }
	        
	        PlayerPrefs.SetInt(Constants.KEY_PLAYER_LEVEL, _currentLevel);
	        _gameLevels[_currentLevel].InitilazeLevel();
        }

        public void RetryCurrentLevel()
        {
	        _gameLevels[_currentLevel].InitilazeLevel();
        }
    }
}

