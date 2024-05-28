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
            Slider.onValueChanged.AddListener(value => OnValueChanged?.Invoke(value));
        }

        public void SetTime(float normalizedTime, float totalTime)
        {
            float elapsedTime = normalizedTime * totalTime;
            ElapsedText.text = (Mathf.Round(elapsedTime * 100f) / 100f).ToString();
            TotalText.text = (Mathf.Round(totalTime * 100f) / 100f).ToString();
            Slider.SetValueWithoutNotify(normalizedTime);
        }
    }
}