using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI.Panels.InstrumentsInfo
{
    public class PanelSizeUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _factText;
        [SerializeField] private RectTransform _factTextTransform;
        [SerializeField] private RectTransform _panelTransform;
        [SerializeField] private float _defaultHeight;
        
        private void Update()
        {
            var size = _panelTransform.sizeDelta;
            size.y = _defaultHeight + _factTextTransform.sizeDelta.y;
            _panelTransform.sizeDelta = size;
        }
    }
}