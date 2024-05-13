﻿using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Instrument Data")]
    public class InstrumentData : ScriptableObject
    {
        public string Name;
        public float InfoButtonHeight;
        public AudioClip Music;
        public string[] Facts;
    }
}