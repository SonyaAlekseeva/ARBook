using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Orchestra : MonoBehaviour
    {
        public float PlayTimer => _playTimer;
        public float PlayTimeSeconds;
        public List<Instrument> Instruments { get; } = new List<Instrument>();

        private bool _isPlaying;
        private float _playTimer;

        private void Update()
        {
            if (!_isPlaying)
                return;
            
            _playTimer += Time.deltaTime;
            if (_playTimer >= PlayTimeSeconds)
            {
                Stop();
                _playTimer = 0;
            }
        }

        public void SetPlayTime(float t)
        {
            _playTimer = PlayTimeSeconds * t;
            
            foreach (Instrument instrument in Instruments)
            {
                if (!instrument.IsMuted)
                    instrument.PlayInOrchestra(_playTimer);
            }
        }

        public void Play()
        {
            foreach (Instrument instrument in Instruments)
            {
                instrument.PlayInOrchestra(_playTimer);
            }

            _isPlaying = true;
        }

        public void Stop()
        {
            foreach (Instrument instrument in Instruments)
            {
                instrument.Stop();
            }

            _isPlaying = false;
        }

        public void AddInstrument(Instrument instrument)
        {
            Instruments.Add(instrument);
            EnableInstrument(instrument);
        }

        public void EnableInstrument(Instrument instrument)
        {
            instrument.IsMuted = false;
            
            if (_isPlaying)
                instrument.PlayInOrchestra(_playTimer);
        }

        public void DisableInstrument(Instrument instrument)
        {
            instrument.IsMuted = true;
            instrument.Stop();
        }
    }
}