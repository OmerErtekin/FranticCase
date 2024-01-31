using UnityEngine;

namespace _Game.Scripts
{
	/// <summary>
	/// Simple follower script that follows on selected axis with an offset
	/// </summary>
    public class Follower : MonoBehaviour
    {
		#region Variables
	    [SerializeField] private Transform _target;
	    [SerializeField] private Vector3 _offset;
	    [SerializeField] private Axis _followAxis;
	    #endregion

	    #region Properties
	    public void SetTarget(Transform target) => _target = target;
	    private bool _willFollowX => (_followAxis & Axis.X) == Axis.X;
	    private bool _willFollowY => (_followAxis & Axis.Y) == Axis.Y;
	    private bool _willFollowZ => (_followAxis & Axis.Z) == Axis.Z;
	    #endregion

	    private void Update()
	    {
		    if (!_target) return;
		    
		    transform.position = new Vector3(_willFollowX ? _target.position.x :transform.position.x
			    , _willFollowY ? _target.position.y : transform.position.y,
			    _willFollowZ ? _target.position.z : transform.position.z) + _offset;
	    }
    }
}

