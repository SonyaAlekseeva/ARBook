using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Instrument Data")]
    public class InstrumentData : ScriptableObject
    {
        [SerializeField] public string Name;
        [SerializeField] public Instrument Model;
        [SerializeField] public AudioClip MusicInOrchestra;
        [SerializeField] public AudioClip MusicSolo;

        [SerializeField] public string[] Facts;
    }
}