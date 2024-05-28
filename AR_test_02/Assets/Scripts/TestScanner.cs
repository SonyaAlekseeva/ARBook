using DefaultNamespace.UI.Panels;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestScanner : MonoBehaviour
    {
        public string TestSpawnName;
        public ScanPanel ScanPanel;
        public InstrumentSpawner Spawner;
        
        [ContextMenu("Test spawn")]
        public void TestSpawn()
        {
            Spawner.ScanInstrument(TestSpawnName);
            Spawner.SpawnInstrument(Vector3.zero, Vector3.up);
        }

        [ContextMenu("Test scan")]
        public void TestScan()
        {
            ScanPanel.TestScan(TestSpawnName);
        }

        [ContextMenu("Test place")]
        public void TestPlace()
        {
            Spawner.SpawnInstrument(Vector3.zero, Vector3.up);
        }
    }
}