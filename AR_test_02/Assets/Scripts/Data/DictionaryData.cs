using DefaultNamespace.UI.Panels;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Dictionary Data")]
    public class DictionaryData : ScriptableObject
    {
        public string Name;
        public string Page;
        public Sprite Illustration;
        [TextArea] public string Description;
        public DictionarySlide FullInfoSlide;
    }
}
