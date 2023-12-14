using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Instrument Data")]
    public class InstrumentData : ScriptableObject
    {
        public string Name;
        public AudioClip MusicInOrchestra;
        public AudioClip MusicSolo;
        public string[] Facts;
    }
}