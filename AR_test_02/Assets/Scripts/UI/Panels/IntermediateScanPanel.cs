using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels
{
    public class IntermediateScanPanel : MonoBehaviour
    {
        public Action OnCompleted;
        
        private enum State
        {
            FadeIn,
            Wait,
            FadeOut,
        }
        
        [SerializeField] private float _fadeInOutTime;
        [SerializeField] private float _waitTime;
        [SerializeField] private float _maxAlpha;
        [SerializeField] private AnimationCurve _alphaCurve;
        [SerializeField] private Image _backgroundImage;

        private float _timer;
        private State _state;

        private void OnEnable()
        {
            _timer = 0f;
            _state = State.FadeIn;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_state == State.FadeIn)
            {
                float t = Mathf.InverseLerp(0f, _fadeInOutTime, _timer);
                if (_timer > _fadeInOutTime)
                {
                    _timer = 0f;
                    _state = State.Wait;
                    t = 1f;
                }
                
                _backgroundImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, _maxAlpha, _alphaCurve.Evaluate(t)));
            }
            else if (_state == State.Wait)
            {
                if (_timer > _waitTime)
                {
                    _timer = 0f;
                    _state = State.FadeOut;
                }
            }
            else
            {
                float t = Mathf.InverseLerp(0f, _fadeInOutTime, _timer);
                if (_timer > _fadeInOutTime)
                {
                    _timer = 0f;
                    _state = State.Wait;
                    t = 1f;
                    gameObject.SetActive(false);
                    OnCompleted?.Invoke();
                }
                
                _backgroundImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, _maxAlpha, _alphaCurve.Evaluate(1 - t)));
            }
        }
    }
}