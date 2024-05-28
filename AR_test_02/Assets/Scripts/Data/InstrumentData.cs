using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Instrument Data")]
    public class InstrumentData : ScriptableObject
    {
        public string Name;
        public float InfoButtonHeight;
        public AudioClip Music;
        public string[] Facts;
        public int[] FactsPages;

        public int GetFactsCountForPage(int page)
        {
            for (int i = 0; i < FactsPages.Length; i++)
            {
                if (FactsPages[i] == page)
                    return i + 1;
            }

            return FactsPages.Length;
        }
    }
}