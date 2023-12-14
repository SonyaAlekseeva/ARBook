using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels
{
    public class OnboardingPage : MonoBehaviour
    {
        public event Action NextClicked;
        public event Action SkipClicked;
        
        public Button NextButton;
        public Button SkipButton;

        private void Start()
        {
            if (NextButton != null)
                NextButton.onClick.AddListener(() => NextClicked?.Invoke());
            
            if (SkipButton != null)
                SkipButton.onClick.AddListener(() => SkipClicked?.Invoke());
        }

        private void Update()
        {
            // TODO: if swipe call NextClicked
        }
    }
}