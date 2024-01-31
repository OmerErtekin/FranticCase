using System.Collections.Generic;
using _Game.Scripts.Player;
using TMPro;
using UnityEngine;

namespace _Game.Scripts
{
    public class Obstacle : MonoBehaviour
    {
    	#region Components
	    [SerializeField] private TMP_Text _healthText;
	    [SerializeField] private MeshRenderer _renderer;
    	#endregion

    	#region Variables
	    [SerializeField] private int _health;
	    [SerializeField] private List<Material> _obstacleMaterials;
	    private int _currentHealth;
        #endregion

        private void OnEnable()
        {
			ResetObstacle();
        }

        private void OnCollisionEnter(Collision other)
        {
	        if (other.transform.TryGetComponent(out PlayerController player))
	        {
		        player.FailTheLevel();
	        }
        }

        private void ResetObstacle()
        {
	        gameObject.SetActive(true);
	        _currentHealth = _health;
	        UpdateMaterialAndText();
        }
        
        public void HitByBullet(Bullet bullet)
        {
	        _currentHealth = Mathf.Max(0, _currentHealth - bullet.Damage);
	        if (_currentHealth == 0)
	        {
		        gameObject.SetActive(false);
		        return;
	        }
            UpdateMaterialAndText();
        }

        private void UpdateMaterialAndText()
        {
	        _healthText.text = $"{_currentHealth}";
	        var healthRateIndex = 3 - (int)((float)_currentHealth * 4 / _health); //set material with health percentage. each %25 has different material
	        _renderer.material = _obstacleMaterials[Mathf.Max(0, healthRateIndex)];
        }
    }
}

