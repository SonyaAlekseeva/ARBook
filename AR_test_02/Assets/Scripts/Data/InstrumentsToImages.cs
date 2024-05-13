using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Instruments To Images")]
    public class InstrumentsToImages : ScriptableObject
    {
        [SerializeField] public InstrumentDataContainer[] Instruments;

        public InstrumentDataContainer GetInstrumentByName(string name)
        {
            foreach (var instrument in Instruments)
            {
                if (instrument.Name == name)
                    return instrument;
            }

            return null;
        }
    }
}