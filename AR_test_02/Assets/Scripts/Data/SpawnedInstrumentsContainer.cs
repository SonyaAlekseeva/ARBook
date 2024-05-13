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
                if (info != null)
                    instrument.Initialize(info);
            }
        }

        public void Register(Orchestra orchestra)
        {
            foreach (var instrument in Instruments)
            {
                orchestra.AddInstrument(instrument);
            }
        }
    }
}