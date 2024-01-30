using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class GameController : MonoBehaviour
    {
    	#region Components
	    public static GameController Instance;
	    [SerializeField] private PlayerController _player;
	    [SerializeField] private ObjectPoolManager _objectPool;
	    #endregion

	    #region Variables
	    public PlayerController Player => _player;
	    public ObjectPoolManager ObjectPool => _objectPool;
	    #endregion

	    private void Awake()
	    {
		    Instance = this;
	    }
    }
}

