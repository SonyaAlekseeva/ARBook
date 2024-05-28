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
        [SerializeField] private Button _prevFactButton;
        [SerializeField] private Button _nextFactButton;
        
        [SerializeField] private PaginationIcon _paginationIconLeft;
        [SerializeField] private PaginationIcon _paginationIconMiddle;
        [SerializeField] private PaginationIcon _paginationIconRight;

        private int _currentFactId;
        private int _availableFactsCount;
        private bool _hasLockedFacts;
        
        private Instrument _currentInstrument;

        private void Awake()
        {
            _nextFactButton.onClick.AddListener(() =>
            {
                SetUpFact(_currentFactId + 1);
            });
            
            _prevFactButton.onClick.AddListener(() =>
            {
                SetUpFact(_currentFactId - 1);
            });
        }

        public void SetUpForInstrument(Instrument instrument)
        {
            _currentInstrument = instrument;
            _availableFactsCount = instrument.Data.GetFactsCountForPage(instrument.Page);
            _hasLockedFacts = instrument.Data.Facts.Length > _availableFactsCount;
            
            Debug.Log($"Initializing instrument {instrument.Name} facts, available: {_availableFactsCount} of {instrument.Data.Facts.Length}");
            SetUpFact(_availableFactsCount - 1);
        }

        private void SetUpFact(int id)
        {
            int maxFactId = _hasLockedFacts ? _availableFactsCount : _availableFactsCount - 1;
            
            _currentFactId = Mathf.Clamp(id, 0, maxFactId);
            _factsNumberText.text = $"Факт {(_currentFactId + 1).ToString()}";
            
            UpdatePaginationForCurrentFact();
            
            if (_hasLockedFacts && _currentFactId >= _availableFactsCount)
            {
                _lockIcon.SetActive(true);
                _factsText.text = LockedFactText;
            }
            else
            {
                _lockIcon.SetActive(false);
                _factsText.text = _currentInstrument.Data.Facts[id];
            }
            
            _nextFactButton.gameObject.SetActive(_currentFactId < maxFactId);
            _prevFactButton.gameObject.SetActive(_currentFactId > 0);
        }

        private void UpdatePaginationForCurrentFact()
        {
            _paginationIconMiddle.gameObject.SetActive(_availableFactsCount > 1);
            _paginationIconRight.gameObject.SetActive(_availableFactsCount > 0);
            
            Debug.Log($"Update pagination, current fact: {_currentFactId}, available: {_availableFactsCount}");
            _paginationIconLeft.SetState(_currentFactId == 0);
            _paginationIconMiddle.SetState(_currentFactId > 0 && _currentFactId < _availableFactsCount);
            _paginationIconRight.SetState(_currentFactId == _availableFactsCount);
        }
    }
}