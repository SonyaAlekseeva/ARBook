using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels.InstrumentsInfo
{
    public class FactsScroller : MonoBehaviour
    {
        private const string LockedFactText = "Сканируйте следующую страницу с этим инструментом и разблокируйте другие факты";
        
        [SerializeField] private TextMeshProUGUI _factsText;
        [SerializeField] private TextMeshProUGUI _factsNumberText;
        [SerializeField] private GameObject _lockIcon;

        [SerializeField] private List<Image> _paginationIcons;

        private int _currentFactId;
        private int _availableFactsCount;
        private bool _hasLockedFacts;
        
        private Instrument _currentInstrument;

        public void SetUpForInstrument(Instrument instrument)
        {
            _currentInstrument = instrument;
            _availableFactsCount = instrument.Data.Facts.Length;
            _hasLockedFacts = _availableFactsCount > instrument.Page + 1;
            
            SetUpFact(instrument.Page);
        }

        private void SetUpFact(int id)
        {
            int maxFactId = _hasLockedFacts ? _availableFactsCount + 1 : _availableFactsCount;
            
            _currentFactId = Mathf.Clamp(id, 0, maxFactId);
            _currentFactId = id;
            _factsNumberText.text = $"Факт {id}";
            
            if (_hasLockedFacts && _currentFactId >= _currentInstrument.Page)
            {
                _lockIcon.SetActive(true);
                _factsText.text = LockedFactText;
            }
            else
            {
                _lockIcon.SetActive(false);
                _factsText.text = _currentInstrument.Data.Facts[id];
            }
        }

        private void Update()
        {
            // TODO: check scroll
        }
    }
}