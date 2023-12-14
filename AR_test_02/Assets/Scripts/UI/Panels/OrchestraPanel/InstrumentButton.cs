using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class InstrumentButton : MonoBehaviour
    {
        public event Action<Instrument> OnInstrumentToggled; 
        
        public TextMeshProUGUI Name;
        
        [Header("Toggle")]
        public Button ToggleButton;
        public Image ButtonImage;
        public Image BackgroundImage;

        [Header("Resources")]
        public Sprite ButtonEnabledSprite;
        public Sprite ButtonDisabledSprite;
        public Sprite BackgroundEnabledSprite;
        public Sprite BackgroundDisabledSprite;

        private Instrument _instrument;
        
        public void Initialize(Instrument instrument)
        {
            ToggleButton.onClick.AddListener(Toggle);
            Name.text = instrument.Data.Name;
            _instrument = instrument;
            SetImagesState(instrument.IsMuted);
        }

        private void Toggle()
        {
            OnInstrumentToggled?.Invoke(_instrument);
            SetImagesState(_instrument.IsMuted);
        }

        private void SetImagesState(bool isMuted)
        {
            ButtonImage.sprite = isMuted ? ButtonDisabledSprite : ButtonEnabledSprite;
            BackgroundImage.sprite = isMuted ? BackgroundDisabledSprite : BackgroundEnabledSprite;
        }
    }
}