using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels.OrchestraPanel
{
    public class OrchestraSlider : MonoBehaviour
    {
        public event Action<float> OnValueChanged;

        public Slider Slider;
        public TextMeshProUGUI ElapsedText;
        public TextMeshProUGUI TotalText;

        private void Start()
        {
            Slider.onValueChanged.AddListener((value) => OnValueChanged?.Invoke(value));
        }

        public void SetTime(float elapsed, float total)
        {
            ElapsedText.text = elapsed.ToString();
            TotalText.text = total.ToString();
            Slider.SetValueWithoutNotify(elapsed / total);
        }
    }
}