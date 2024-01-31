using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Controllers
{
    public class ObjectPoolController : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private Bullet _bulletPrefab;
	    [SerializeField] private int _initialPoolSize = 10;
    	#endregion

    	#region Variables
	    private readonly List<Bullet> _pooledBullets = new();
        #endregion

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
	        for (var i = 0; i < _pooledBullets.Count; i++)
	        {
		        if (_pooledBullets[i].gameObject.activeInHierarchy) continue;
		        
		        _pooledBullets[i].gameObject.SetActive(true);
		        return _pooledBullets[i];
	        }

	        //If there is no available bullet, spawn new ones
	        var obj = Instantiate(_bulletPrefab,transform);
	        _pooledBullets.Add(obj);
	        return obj;
        }
    }
}

