using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels
{
    public class DictionaryFrame : MonoBehaviour
    {
        public event Action<DictionaryData> OnOpenClick;
        
        public Image MainImage;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Page;
        public TextMeshProUGUI Description;
        public Button Button;

        private DictionaryData _data;

        public void Initialize(DictionaryData data)
        {
            _data = data;
            Button.onClick.AddListener(() => OnOpenClick?.Invoke(_data));

            MainImage.sprite = data.Illustration;
            Name.text = data.Name;
            Page.text = data.Page;
            Description.text = data.Description;
        }
    }
}