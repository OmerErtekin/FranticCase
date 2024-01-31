using UnityEngine;

namespace _Game.Scripts.Controllers
{
    public class LevelController : MonoBehaviour
    {
    	#region Components
    	#endregion

    	#region Variables
        #endregion

        private void Start()
        {
	        EventManager.TriggerEvent(EventManager.OnLevelInitialized);
        }

        private void Update()
        {
	        if (Input.GetKeyDown(KeyCode.S))
	        {
		        EventManager.TriggerEvent(EventManager.OnLevelStarted);
	        }
	        
	        if (Input.GetKeyDown(KeyCode.D))
	        {
		        EventManager.TriggerEvent(EventManager.OnLevelCompleted);
	        }
        }
    }
}

