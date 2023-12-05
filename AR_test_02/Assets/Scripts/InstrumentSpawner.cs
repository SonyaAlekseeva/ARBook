using UnityEngine;

namespace DefaultNamespace
{
    public class InstrumentSpawner : MonoBehaviour
    {
        public Orchestra Orchestra;
        public InstrumentData[] instruments;

        private int _scannedId;
        private int _scannedPage;

        public void ScanInstrument(int id, int page)
        {
            _scannedId = id;
            _scannedPage = page;
        }

        public void SpawnInstrument(Vector3 position)
        {
            if (_scannedId < 0)
                return;

            InstrumentData data = instruments[_scannedId];
            
            Instrument instrument = Instantiate(data.Model);
            instrument.Initialize(data, _scannedPage, position);
            
            Orchestra.AddInstrument(instrument);
        }
    }
}