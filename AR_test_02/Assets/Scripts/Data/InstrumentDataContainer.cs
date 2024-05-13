using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class InstrumentDataContainer
    {
        [SerializeField] public string Name;
        [SerializeField] public SpawnedInstrumentsContainer Target;
        [SerializeField] public List<InstrumentOnImageInfo> InstrumentsInfo;

        public InstrumentOnImageInfo GetInstrumentInfo(string name)
        {
            foreach (var info in InstrumentsInfo)
            {
                if (info.InstrumentName == name)
                    return info;
            }

            return null;
        }
    }
}