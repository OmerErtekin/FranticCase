using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.Controllers
{
	[DefaultExecutionOrder(-99)]
    public class GameController : MonoBehaviour
    {
    	#region Components
	    public static GameController Instance;
	    [SerializeField] private PlayerController _player;
	    [SerializeField] private ObjectPoolController _objectPool;
	    [SerializeField] private GameUIController _uiController;
	    [SerializeField] private LevelController _levelController;
	    #endregion

	    #region Variables
	    public PlayerController Player => _player;
	    public ObjectPoolController ObjectPool => _objectPool;
	    public GameUIController UIController => _uiController;
	    public LevelController LevelController => _levelController;
	    #endregion

	    private void Awake()
	    {
		    Instance = this;
	    }
    }
}

