using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace  _Game.Scripts.UI
{
    public class FadeableUI : MonoBehaviour
    {
        #region Components
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _image;
        #endregion

        #region Variables
        private Tween _fadeInTween, _fadeOutTween;
        private Coroutine _showAndHideRoutine;
        #endregion
        
        public void FadeIn(float fadeInDuration,bool willActivate = true, Action onFadeCompleted = null,float delay = 0)
        {
            _fadeInTween?.Kill();
            _fadeOutTween?.Kill();
            if (willActivate)
            {
                gameObject.SetActive(true);
            }
            
            if (_canvasGroup)
            {
                _fadeInTween = _canvasGroup.DOFade(1, fadeInDuration).SetTarget(this).From(0).SetUpdate(true).SetDelay(delay);
            }
            else if (_image)
            {
                _fadeInTween = _image.DOFade(1, fadeInDuration).SetTarget(this).From(0).SetUpdate(true).SetDelay(delay);
            }

            _fadeInTween?.OnComplete(() => onFadeCompleted?.Invoke());
        }

        public void FadeOut(float fadeOutDuration, bool willDeactivate = true, Action onFadeCompleted = null,float delay = 0)
        {
            _fadeInTween?.Kill();
            _fadeOutTween?.Kill();
            if (_canvasGroup)
            {
                _fadeOutTween = _canvasGroup.DOFade(0, fadeOutDuration).SetTarget(this).From(1).SetUpdate(true).SetDelay(delay);
            }
            
            if (_image)
            {
                _fadeOutTween = _image.DOFade(0, fadeOutDuration).SetTarget(this).From(1).SetUpdate(true).SetDelay(delay);
            }

            _fadeOutTween?.OnComplete(() =>
            {
                if (willDeactivate)
                {
                    gameObject.SetActive(false);
                }
                
                onFadeCompleted?.Invoke();
            });
        }

        public void FadeOutDirectly()
        {
            _fadeInTween?.Kill();
            _fadeOutTween?.Kill();
            if (_canvasGroup)
            {
                _canvasGroup.alpha = 0;
            }
            
            if (_image)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
            }
            
            gameObject.SetActive(false);
        }
    }
}

