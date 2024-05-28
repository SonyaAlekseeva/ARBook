using UnityEngine;

namespace DefaultNamespace
{
    public class SpawnedInstrumentsContainer : MonoBehaviour
    {
        public Instrument[] Instruments;

        public void Initialize(InstrumentDataContainer instrumentDataContainer)
        {
            foreach (var instrument in Instruments)
            {
                var info = instrumentDataContainer.GetInstrumentInfo(instrument.Name);
                Debug.Log($"Initializing instrument {instrument.Name}, found info: {info != null}");
                
                if (info != null)
                    instrument.Initialize(info, instrumentDataContainer.Name);
            }
        }

        public void Register(Orchestra orchestra)
        {
            foreach (var instrument in Instruments)
            {
                Debug.Log($"Registering instrument {instrument.name} in orchestra");
                orchestra.AddInstrument(instrument);
            }
        }
    }
}