using UnityEngine;

namespace DefaultNamespace
{
    public class SpawnedInstrumentsContainer : MonoBehaviour
    {
        public Instrument[] Instruments;

        public void Initialize(int page)
        {
            foreach (var instrument in Instruments)
            {
                instrument.Initialize(page);
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