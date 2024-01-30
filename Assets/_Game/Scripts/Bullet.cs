using UnityEngine;

namespace _Game.Scripts
{
    public class Bullet : MonoBehaviour
    {
    	#region Components
    	#endregion

    	#region Variables
	    [SerializeField] private float _baseLifeTime;
	    private bool _canBounce,_isExploded;
	    private float _currentLifeTime,_currentSpeed;
	    private int _currentDamage,_currentBounceCount;
	    #endregion
        
	    public void SetBullet(int damage,int bounceCount,float speed,Transform firePos)
	    {
		    _isExploded = false;
		    _currentLifeTime = _baseLifeTime;
		    _currentBounceCount = bounceCount;
		    _currentSpeed = speed;
		    _currentDamage = damage;
		    transform.position = firePos.position;
		    transform.rotation = firePos.rotation;
	    }

	    private void Update()
	    {
		    MoveForward();
	    }

	    private void MoveForward()
	    {
		    if (_currentLifeTime <= 0)
		    {
			    Explode();
			    return;
		    }

		    _currentLifeTime -= Time.deltaTime;
		    transform.position += transform.forward * (_currentSpeed * Time.deltaTime);
	    }

	    private void Explode()
	    {
		    if(_isExploded) return;

		    _isExploded = true;
		    gameObject.SetActive(false);
	    }
    }
}

