using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels.InstrumentsInfo
{
    public class InstrumentInfo : MonoBehaviour
    {
        public event Action<bool, InstrumentInfo> OnInfoClicked;
        public bool FactsActive { get; private set; }

        [HideInInspector] public Instrument Instrument;
        
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _factsTransform;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Button _toggleInfoButton;
        [SerializeField] private FactsScroller _factsScroller;
        [SerializeField] private bool _adjustHalfScreenSize;
        [SerializeField] private Canvas _canvas;
        
        private bool _hasInstrument;

        private Vector2 _halfScreenSize;
        private Vector2 _factsOffset;
        private RectTransform _panelTransform;

        public void SetUpForInstrument(Instrument instrument)
        {
            Instrument = instrument;
            _factsScroller.SetUpForInstrument(instrument);
            _nameText.text = instrument.Data.Name;
            _hasInstrument = true;
            _canvas.worldCamera = Camera.main;
        }

        public void SetPanel(RectTransform panel) => _panelTransform = panel;
        
        private void Awake()
        {
            _toggleInfoButton.onClick.AddListener(InfoClicked);
            _halfScreenSize = new Vector3(Screen.width, Screen.height) * 0.5f;
            _factsOffset = _factsTransform.anchoredPosition - _rectTransform.anchoredPosition;
        }

        private void Update()
        {
            if (!_hasInstrument) return;
            
            var camera = Camera.main;
            if (camera == null) return;

            transform.rotation = Quaternion.LookRotation(camera.transform.forward, Vector3.up);

            // if (_panelTransform == null) return;
            //
            // var position = Instrument.transform.position + Vector3.up * Instrument.Data.InfoButtonHeight;
            // var screenPosition = camera.WorldToScreenPoint(position);
            // // _rectTransform.anchoredPosition = screenPosition;
            // // if (_adjustHalfScreenSize)
            // //     _rectTransform.anchoredPosition -= _halfScreenSize;
            //
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(_panelTransform, screenPosition, camera,
            //     out var point);
            //
            // _rectTransform.localPosition = point;
            //
            // // _factsTransform.anchoredPosition = _rectTransform.anchoredPosition + _factsOffset;
        }

        public void SetFactsState(bool state)
        {
            FactsActive = state;
            _factsScroller.gameObject.SetActive(FactsActive);
        }

        private void InfoClicked()
        {
            OnInfoClicked?.Invoke(!FactsActive, this);
        }
    }
}