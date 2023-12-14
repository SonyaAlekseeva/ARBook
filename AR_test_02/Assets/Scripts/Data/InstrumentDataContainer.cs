using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class InstrumentDataContainer
    {
        [SerializeField] public string Name;
        [SerializeField] public SpawnedInstrumentsContainer Target;
    }
}