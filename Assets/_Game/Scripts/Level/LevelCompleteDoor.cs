using System.Collections.Generic;
using _Game.Scripts.Level;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts
{
    public class LevelCompleteDoor : BaseDoor
    {
    	#region Variables
	    [SerializeField] private List<ParticleSystem> _fireworkParticles;
        #endregion

        protected override void ResetDoor()
        {
	        foreach (var particle in _fireworkParticles)
	        {
		        particle.Stop();
	        }
        }

        protected override void OnContactedWithPlayer(PlayerController player)
        {
	        base.OnContactedWithPlayer(player);
	        player.WinTheLevel();
	        foreach (var particle in _fireworkParticles)
	        {
		        particle.Play();
	        }
        }
    }
}

