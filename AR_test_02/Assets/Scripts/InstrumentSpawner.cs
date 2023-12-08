using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class InstrumentSpawner : MonoBehaviour
    {
        public Orchestra Orchestra;
        public InstrumentsToImages Instruments;
        
        private InstrumentData _scannedInstrument;
        private int _scannedPage;

        public void ScanInstrument(string imageName)
        {
            string[] values = imageName.Split("_");
            string name = values[0];
            _scannedPage = int.Parse(values[1]);
            _scannedInstrument = Instruments.GetInstrumentByName(name);
        }

        public void SpawnInstrument(Vector3 position)
        {
            if (_scannedInstrument == null)
                return;
            
            Instrument instrument = Instantiate(_scannedInstrument.Model);
            instrument.Initialize(_scannedInstrument, _scannedPage, position);
            
            Orchestra.AddInstrument(instrument);
        }
    }
}