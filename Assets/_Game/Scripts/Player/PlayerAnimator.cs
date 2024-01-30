using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Managers;
using DG.Tweening;
using RootMotion.FinalIK;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
    	#region Components
	    private Animator _animator;
	    private AimIK _aimIK;
	    private FullBodyBipedIK _bodyIK;
    	#endregion

    	#region Variables
	    private readonly List<int> _animationHashes = new();
	    private PlayerAnims _lastAnim;
	    private Tween _layerWeightTween,_ikWeightTween;
        #endregion

        private void OnEnable()
        {
	        EventManager.StartListening(EventManager.OnPlayerStartToMove,SetAnimatorOnMovementStart);
	        EventManager.StartListening(EventManager.OnPlayerStop,SetAnimatorOnMovementStop);
        }

        private void OnDisable()
        {
	        EventManager.StopListening(EventManager.OnPlayerStartToMove,SetAnimatorOnMovementStart);
	        EventManager.StopListening(EventManager.OnPlayerStop,SetAnimatorOnMovementStop);
        }

        private void Start()
        {
	        _animator = GetComponent<Animator>();
	        _aimIK = GetComponent<AimIK>();
	        _bodyIK = GetComponent<FullBodyBipedIK>();
        }

        private void ConvertAnimsToHash()
        {
	        _animationHashes.Clear();
	        var maxEnumValue = Enum.GetValues(typeof(PlayerAnims)).Cast<int>().Max();
	        for (var i = 0; i <= maxEnumValue; i++)
	        {
		        _animationHashes.Add(Animator.StringToHash(Enum.GetName(typeof(PlayerAnims), i)));
	        }
        }

        private void SetAnimatorOnMovementStart()
        {
	        SetSecondLayerWeight(1,0.25f);
	        SetIKWeights(1, 0.5f);
        }

        private void SetAnimatorOnMovementStop()
        {
	        SetSecondLayerWeight(0,0.1f);
	        SetIKWeights(0,0.1f);
        }
        
        public void PlayAnimation(PlayerAnims anim, float fadeDuration = 0.1f, bool willReset = false)
        {
	        if(_lastAnim == anim && !willReset) return;
	        
	        if (_animationHashes.Count == 0)
	        {
		        ConvertAnimsToHash();
	        }

	        _lastAnim = anim;
	        if (fadeDuration > 0 && !willReset)
	        {
		        _animator.CrossFadeInFixedTime(_animationHashes[(int)anim], fadeDuration);
	        }
	        else
	        {
		        _animator.Play(_animationHashes[(int)anim]);
	        }
        }

        private void SetSecondLayerWeight(float target, float duration)
        {
	        var currentWeight = _animator.GetLayerWeight(1);
	        _layerWeightTween?.Kill();
	        _layerWeightTween = DOTween.To(() => currentWeight, x => currentWeight = x, target, duration)
		        .OnUpdate(() => _animator.SetLayerWeight(1, currentWeight));
        }

        private void SetIKWeights(float target, float duration)
        {
	        var currentWeight = _aimIK.solver.IKPositionWeight;
	        _ikWeightTween?.Kill();
	        _ikWeightTween = DOTween.To(() => currentWeight, x => currentWeight = x, target, duration)
		        .OnUpdate(() =>
		        {
			        _aimIK.solver.IKPositionWeight = currentWeight;
			        _bodyIK.solver.IKPositionWeight = currentWeight;
		        });
        }
    }
}

