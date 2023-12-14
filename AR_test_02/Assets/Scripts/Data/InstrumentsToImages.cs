using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Instruments To Images")]
    public class InstrumentsToImages : ScriptableObject
    {
        [SerializeField] public InstrumentDataContainer[] Instruments;

        public SpawnedInstrumentsContainer GetInstrumentByName(string name)
        {
            foreach (var instrument in Instruments)
            {
                if (instrument.Name == name)
                    return instrument.Target;
            }

            return null;
        }
    }
}