using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Controllers
{
    public class ObjectPoolController : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private Bullet _bulletPrefab;
    	#endregion

    	#region Variables
	    [SerializeField] private int _initialPoolSize = 10;
	    private readonly List<Bullet> _pooledBullets = new();
        #endregion

        private void OnEnable()
        {
	        EventManager.StartListening(EventManager.OnLevelInitialized,ResetPool);
        }

        private void OnDisable()
        {
	        EventManager.StopListening(EventManager.OnLevelInitialized,ResetPool);
        }

        private void Start()
        {
	        for (var i = 0; i < _initialPoolSize; i++)
	        {
		        var obj = Instantiate(_bulletPrefab, transform);
		        obj.gameObject.SetActive(false);
		        _pooledBullets.Add(obj);
	        }
        }
        
        public Bullet GetBullet()
        {
	        foreach (var bullet in _pooledBullets)
	        {
		        if (bullet.gameObject.activeInHierarchy) continue;
		        
		        bullet.gameObject.SetActive(true);
		        return bullet;
	        }

	        //If there is no available bullet, spawn new ones
	        var obj = Instantiate(_bulletPrefab,transform);
	        _pooledBullets.Add(obj);
	        return obj;
        }

        private void ResetPool()
        {
	        foreach (var bullet in _pooledBullets)
	        {
		        bullet.gameObject.SetActive(false);
	        }
        }
    }
}

