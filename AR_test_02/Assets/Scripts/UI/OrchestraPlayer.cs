using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class OrchestraPlayer : MonoBehaviour
    {
        public Orchestra Orchestra;
        
        [Header("Play Control")]
        public Button PlayButton;
        public Button PauseButton;
        public Slider PlaySlider;
        
        [Header("Instruments View")]
        public Button ShowInstrumentsViewButton;
        public Button HideInstrumentsViewButton;
        public GameObject MinimalisticView;
        public GameObject ExpandedView;

        private void Awake()
        {
            PlayButton.onClick.AddListener(Play);
            PauseButton.onClick.AddListener(Pause);
            PlaySlider.onValueChanged.AddListener(SetPlayTime);
            ShowInstrumentsViewButton.onClick.AddListener(ShowInstrumentsView);
            HideInstrumentsViewButton.onClick.AddListener(HideInstrumentsView);
        }

        private void Update()
        {
            PlaySlider.SetValueWithoutNotify(Orchestra.PlayTimerNormalized);
        }

        private void SetPlayTime(float t)
        {
            Orchestra.SetPlayTime(t);
        }

        private void Play()
        {
            Orchestra.Play();
        }

        private void Pause()
        {
            Orchestra.Stop();
        }
        
        private void ShowInstrumentsView()
        {
            MinimalisticView.SetActive(false);
            ExpandedView.SetActive(true);
        }

        private void HideInstrumentsView()
        {
            MinimalisticView.SetActive(true);
            ExpandedView.SetActive(false);
        }
    }
}