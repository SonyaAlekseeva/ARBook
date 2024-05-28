using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class InstrumentOnImageInfo
    {
        [SerializeField] public string InstrumentName;
        [SerializeField] public AudioClip MusicClip;
    }
}