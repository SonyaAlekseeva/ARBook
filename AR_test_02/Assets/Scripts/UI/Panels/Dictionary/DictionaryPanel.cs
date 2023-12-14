using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels
{
    public class DictionaryPanel : MonoBehaviour
    {
        [Header("Control")]
        [SerializeField] private Button BackButton;
        [SerializeField] private MainPanel MainPanel;
        [SerializeField] private Transform FullInfoSlidesParent;
        
        [Header("Frames")]
        [SerializeField] private Transform FramesParent;
        [SerializeField] private DictionaryFrame FramePrefab;
        [SerializeField] private DictionaryData[] FramesData;

        private DictionarySlide _currentFullInfoSlide;

        private void Start()
        {
            foreach (var data in FramesData)
            {
                var frame = Instantiate(FramePrefab, FramesParent);
                frame.Initialize(data);
                frame.OnOpenClick += OpenFrame;
            }
            
            BackButton.onClick.AddListener(Close);
        }

        private void OpenFrame(DictionaryData data)
        {
            _currentFullInfoSlide = Instantiate(data.FullInfoSlide, FullInfoSlidesParent);
            _currentFullInfoSlide.OnBackClick += CloseFrame;
        }

        private void CloseFrame()
        {
            _currentFullInfoSlide.OnBackClick -= CloseFrame;
            Destroy(_currentFullInfoSlide.gameObject);
            _currentFullInfoSlide = null;
        }

        private void Close()
        {
            gameObject.SetActive(false);
            MainPanel.gameObject.SetActive(true);
        }
    }
}