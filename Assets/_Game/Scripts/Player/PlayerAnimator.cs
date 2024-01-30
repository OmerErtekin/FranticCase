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

        #region Properties
        public bool IsIKEnabled { get; private set; }
        #endregion

        private void OnEnable()
        {
	        EventManager.StartListening(EventManager.OnPlayerStartToMove,SetAnimatorOnMovementStart);
	        EventManager.StartListening(EventManager.OnPlayerStop,SetAnimatorOnMovementStop);
	        EventManager.StartListening(EventManager.OnWeaponEquiped,UpdateIKForWeapon);
        }

        private void OnDisable()
        {
	        EventManager.StopListening(EventManager.OnPlayerStartToMove,SetAnimatorOnMovementStart);
	        EventManager.StopListening(EventManager.OnPlayerStop,SetAnimatorOnMovementStop);
	        EventManager.StopListening(EventManager.OnWeaponEquiped,UpdateIKForWeapon);
        }

        private void Awake()
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
	        SetSecondLayerAndIK(1,0.25f);
        }

        private void SetAnimatorOnMovementStop()
        {
	        SetSecondLayerAndIK(0,0.1f);
        }

        private void UpdateIKForWeapon(Weapon weapon)
        {
	        _bodyIK.solver.leftHandEffector.target = weapon.LeftHandTargetTransform;
	        _aimIK.solver.transform = weapon.AimTargetTransform;
        }
        
        //Instead of creating connections between animations on animator controller,
        //I use more programatic way. With this approach, we don't need to have connections between animations.
        //Just add animations to controller and name it same with enum. Then we can play and pass to other anims smoothly.
        public void PlayAnimation(PlayerAnims anim, float changeDuration = 0.1f, bool willReset = false)
        {
	        if(_lastAnim == anim && !willReset) return;
	        
	        if (_animationHashes.Count == 0)
	        {
		        ConvertAnimsToHash();
	        }

	        _lastAnim = anim;
	        if (changeDuration > 0 && !willReset)
	        {
		        _animator.CrossFadeInFixedTime(_animationHashes[(int)anim], changeDuration); //will change smoothly in given time
	        }
	        else
	        {
		        _animator.Play(_animationHashes[(int)anim]); //directly plays animation from start
	        }
        }

        private void SetSecondLayerAndIK(float target, float duration)
        {
	        var currentWeight = _animator.GetLayerWeight(1);
	        _layerWeightTween?.Kill();
	        _layerWeightTween = DOTween.To(() => currentWeight, x => currentWeight = x, target, duration)
		        .OnUpdate(() => _animator.SetLayerWeight(1, currentWeight)).SetTarget(this);
	        
	        IsIKEnabled = false;
	        var currenIKtWeight = _aimIK.solver.IKPositionWeight;
	        _ikWeightTween?.Kill();
	        _ikWeightTween = DOTween.To(() => currenIKtWeight, x => currenIKtWeight = x, target, duration)
		        .OnUpdate(() =>
		        {
			        _aimIK.solver.IKPositionWeight = currenIKtWeight;
			        _bodyIK.solver.IKPositionWeight = currenIKtWeight;
		        }).OnComplete(() =>
		        {
			        IsIKEnabled = true;
		        }).SetTarget(this);
        }
    }
}

