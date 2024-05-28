using UnityEngine;

namespace DefaultNamespace.UI.Panels
{
    public class OnboardingPanel : MonoBehaviour
    {
        public bool ReturnToMainPanel;
        public GameObject ScanPanel;
        public GameObject MainPanel;
        public OnboardingPage[] Pages;

        private int _currentPageId;
        private OnboardingPage _currentPage;
        
        private void OnEnable()
        {
            foreach (var page in Pages)
            {
                page.gameObject.SetActive(false);
            }
            
            SetPage(0);
        }

        private void SetPage(int id)
        {
            if (_currentPage != null)
            {
                _currentPage.gameObject.SetActive(false);
                
                _currentPage.NextClicked -= NextPage;
                _currentPage.SkipClicked -= Skip;
            }

            var page = Pages[id];
            
            _currentPageId = id;
            _currentPage = page;
            _currentPage.gameObject.SetActive(true);

            _currentPage.NextClicked += NextPage;
            _currentPage.SkipClicked += Skip;
        }

        private void NextPage()
        {
            if (_currentPageId >= Pages.Length - 1)
            {
                Skip();
                return;
            }
            
            SetPage(_currentPageId + 1);
        }

        private void Skip()
        {
            gameObject.SetActive(false);
            if (ReturnToMainPanel)
                MainPanel.SetActive(true);
            else
                ScanPanel.SetActive(true);

            ReturnToMainPanel = false;
        }
    }
}