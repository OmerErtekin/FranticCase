using UnityEngine;

namespace _Game.Scripts
{
    public class CamFollower : MonoBehaviour
    {
		#region Variables
	    [SerializeField] private Transform _target;
	    [SerializeField] private Vector3 _offset;
	    #endregion

	    private void Update()
	    {
		    transform.position = new Vector3(transform.position.x, _target.position.y, _target.position.z) + _offset;
	    }
    }
}

