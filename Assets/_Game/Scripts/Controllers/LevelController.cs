using System.Collections.Generic;
using _Game.Scripts.Level;
using UnityEngine;

namespace _Game.Scripts.Controllers
{
    public class LevelController : MonoBehaviour
    {
    	#region Components
    	#endregion

    	#region Variables
	    [SerializeField] private List<LevelParent> _gameLevels;
        #endregion

        private void Start()
        {
	        _gameLevels[0].InitilazeLevel();
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
    }
}

