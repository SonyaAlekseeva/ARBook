using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels
{
    public class DictionarySlide : MonoBehaviour
    {
        public event Action OnBackClick;
        
        public Button BackButton;

        private void Start()
        {
            BackButton.onClick.AddListener(() => OnBackClick?.Invoke());
        }
    }
}