using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Dictionary Data")]
    public class DictionaryData : ScriptableObject
    {
        [SerializeField] public int Number;
        [SerializeField] public string Name;
        [SerializeField] public string Page;
        [SerializeField] public GameObject Illustration;
        [SerializeField] public string Description;

    }
}
