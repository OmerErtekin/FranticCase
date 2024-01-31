using System.Collections.Generic;
using _Game.Scripts.Level;
using UnityEngine;

namespace _Game.Scripts
{
    public class Bullet : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private ParticleSystem _explosionParticle;
    	#endregion

    	#region Variables
	    [SerializeField] private List<GameObject> _bulletModels;
	    [SerializeField] private float _baseLifeTime;
	    private bool _isExploded;
	    private float _currentLifeTime,_currentSpeed;
	    private int _currentDamage,_currentBounceCount;
	    #endregion

	    #region Properties
	    public int Damage => _currentDamage;
	    #endregion
        
	    public void SetBullet(int damage,int bounceCount,float speed,Transform firePos,WeaponTypes type)
	    {
		    for (var i = 0; i < _bulletModels.Count; i++)
		    {
			    _bulletModels[i].SetActive(i == (int)type);
		    }
		    
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
	    
	    private void OnCollisionEnter(Collision other)
	    {
		    //Bullets can only contact with walls & obstacles. It can be set on Bullet/Rigidbody/LayerOverrides
		    if (other.transform.TryGetComponent(out Obstacle obstacle))
		    {
			    obstacle.HitByBullet(this);
		    }
		    
		    TryBounce(other.contacts[0].normal);
	    }

	    private void MoveForward()
	    {
		    if (_currentLifeTime <= 0)
		    {
			    Explode(false);
			    return;
		    }

		    _currentLifeTime -= Time.deltaTime;
		    transform.position += transform.forward * (_currentSpeed * Time.deltaTime);
	    }

	    private void Explode(bool willPlayParticle)
	    {
		    if(_isExploded) return;

		    _isExploded = true;
		    if (willPlayParticle)
		    {
			    _explosionParticle.transform.parent = null;
			    _explosionParticle.transform.SetPositionAndRotation(transform.position,transform.rotation);
			    _explosionParticle.Play();
		    }
		    gameObject.SetActive(false);
	    }

	    private void TryBounce(Vector3 hitNormal)
	    {
		    if (_currentBounceCount <= 0)
		    {
			    Explode(true);
                return;
		    }

		    _currentBounceCount--;
		    var newDirection = Vector3.Reflect(transform.forward, hitNormal);
		    transform.forward = newDirection;
	    }
    }
}

